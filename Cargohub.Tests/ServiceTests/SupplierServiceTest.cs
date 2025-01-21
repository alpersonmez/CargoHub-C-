using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cargohub.Models;
using Cargohub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargohub.Tests
{
    [TestClass]
    public class SupplierServiceTests
    {
        private AppDbContext _context;
        private SupplierService _supplierService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _supplierService = new SupplierService(_context);

            // Seed the in-memory database
            _context.Supplier.AddRange(new List<Supplier>
            {
                new Supplier
                {
                    id = 1,
                    code = "SUP001",
                    name = "Supplier 1",
                    address = "123 Main St",
                    city = "City A",
                    zip_code = "12345",
                    country = "Country A",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Supplier
                {
                    id = 2,
                    code = "SUP002",
                    name = "Supplier 2",
                    address = "456 Elm St",
                    city = "City B",
                    zip_code = "67890",
                    country = "Country B",
                    created_at = DateTime.UtcNow.AddDays(-20),
                    updated_at = DateTime.UtcNow.AddDays(-10),
                    isdeleted = false
                }
            });
            _context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAllSuppliers_ReturnsCorrectAmount()
        {
            // Act
            var suppliers = await _supplierService.GetAllSuppliers(2);

            // Assert
            Assert.AreEqual(2, suppliers.Count);
        }

        [TestMethod]
        public async Task GetSupplierById_ReturnsCorrectSupplier()
        {
            // Act
            var supplier = await _supplierService.GetSupplierById(1);

            // Assert
            Assert.IsNotNull(supplier);
            Assert.AreEqual("SUP001", supplier.code);
            Assert.AreEqual("Supplier 1", supplier.name);
        }

        [TestMethod]
        public async Task GetSupplierById_ReturnsNull_WhenSupplierDoesNotExist()
        {
            // Act
            var supplier = await _supplierService.GetSupplierById(99);

            // Assert
            Assert.IsNull(supplier);
        }

        [TestMethod]
        public async Task AddSupplier_AddsSupplierSuccessfully()
        {
            // Arrange
            var newSupplier = new Supplier
            {
                code = "SUP003",
                name = "Supplier 3",
                address = "789 Oak St",
                city = "City C",
                zip_code = "54321",
                country = "Country C",
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            // Act
            var addedSupplier = await _supplierService.AddSupplier(newSupplier);

            // Assert
            Assert.IsNotNull(addedSupplier);
            Assert.AreEqual("SUP003", addedSupplier.code);
            Assert.AreEqual(3, _context.Supplier.Count());
        }

        [TestMethod]
        public async Task UpdateSupplier_UpdatesExistingSupplierSuccessfully()
        {
            // Arrange
            var existingSupplier = await _supplierService.GetSupplierById(1);
            existingSupplier.name = "Updated Supplier 1";

            // Act
            var updated = await _supplierService.UpdateSupplier(existingSupplier);

            // Assert
            Assert.IsTrue(updated);
            var updatedSupplier = await _supplierService.GetSupplierById(1);
            Assert.AreEqual("Updated Supplier 1", updatedSupplier.name);
        }

        [TestMethod]
        public async Task UpdateSupplier_ReturnsFalse_WhenSupplierDoesNotExist()
        {
            // Arrange
            var nonExistingSupplier = new Supplier
            {
                id = 99,
                name = "Nonexistent Supplier",
                code = "SUP999"
            };

            // Act
            var updated = await _supplierService.UpdateSupplier(nonExistingSupplier);

            // Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public async Task DeleteSupplier_SoftDeletesSupplierSuccessfully()
        {
            // Act
            var deleted = await _supplierService.DeleteSupplier(1);

            // Assert
            Assert.IsTrue(deleted);
            var supplier = await _supplierService.GetSupplierById(1);
            Assert.IsTrue(supplier.isdeleted);
        }

        [TestMethod]
        public async Task DeleteSupplier_ReturnsFalse_WhenSupplierDoesNotExist()
        {
            // Act
            var deleted = await _supplierService.DeleteSupplier(99);

            // Assert
            Assert.IsFalse(deleted);
        }
    }
}
