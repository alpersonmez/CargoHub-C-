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
    public class TransferServiceTests
    {
        private AppDbContext _context;
        private TransferService _transferService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _transferService = new TransferService(_context);

            // Seed the in-memory database
            _context.Transfers.AddRange(new List<Transfer>
            {
                new Transfer
                {
                    id = 1,
                    reference = "REF001",
                    transfer_from = 100,
                    transfer_to = 200,
                    transfer_status = "Pending",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    isdeleted = false
                },
                new Transfer
                {
                    id = 2,
                    reference = "REF002",
                    transfer_from = 101,
                    transfer_to = 201,
                    transfer_status = "Completed",
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
        public async Task GetTransfers_ReturnsCorrectAmount()
        {
            // Act
            var transfers = await _transferService.GetTransfers(2);

            // Assert
            Assert.AreEqual(2, transfers.Count);
        }

        [TestMethod]
        public async Task GetTransfer_ReturnsCorrectTransfer()
        {
            // Act
            var transfer = await _transferService.GetTransfer(1);

            // Assert
            Assert.IsNotNull(transfer);
            Assert.AreEqual("REF001", transfer.reference);
        }

        [TestMethod]
        public async Task GetTransfer_ReturnsNull_WhenTransferDoesNotExist()
        {
            // Act
            var transfer = await _transferService.GetTransfer(99);

            // Assert
            Assert.IsNull(transfer);
        }

        [TestMethod]
        public async Task AddTransfer_AddsTransferSuccessfully()
        {
            // Arrange
            var newTransfer = new Transfer
            {
                reference = "REF003",
                transfer_from = 102,
                transfer_to = 202,
                transfer_status = "In Progress"
            };

            // Act
            var addedTransfer = await _transferService.AddTransfer(newTransfer);

            // Assert
            Assert.IsNotNull(addedTransfer);
            Assert.AreEqual("REF003", addedTransfer.reference);
            Assert.AreEqual(3, _context.Transfers.Count());
        }

        [TestMethod]
        public async Task UpdateTransfer_UpdatesExistingTransferSuccessfully()
        {
            // Arrange
            var existingTransfer = await _transferService.GetTransfer(1);
            existingTransfer.transfer_status = "Completed";

            // Act
            var updated = await _transferService.UpdateTransfer(existingTransfer);

            // Assert
            Assert.IsTrue(updated);
            var updatedTransfer = await _transferService.GetTransfer(1);
            Assert.AreEqual("Completed", updatedTransfer.transfer_status);
        }

        [TestMethod]
        public async Task UpdateTransfer_ReturnsFalse_WhenTransferDoesNotExist()
        {
            // Arrange
            var nonExistingTransfer = new Transfer
            {
                id = 99,
                reference = "REF999",
                transfer_from = 103,
                transfer_to = 203,
                transfer_status = "Nonexistent"
            };

            // Act
            var updated = await _transferService.UpdateTransfer(nonExistingTransfer);

            // Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public async Task DeleteTransfer_SoftDeletesTransferSuccessfully()
        {
            // Act
            var deleted = await _transferService.DeleteTransfer(1);

            // Assert
            Assert.IsTrue(deleted);
            var transfer = await _transferService.GetTransfer(1);
            Assert.IsTrue(transfer.isdeleted);
        }

        [TestMethod]
        public async Task DeleteTransfer_ReturnsFalse_WhenTransferDoesNotExist()
        {
            // Act
            var deleted = await _transferService.DeleteTransfer(99);

            // Assert
            Assert.IsFalse(deleted);
        }
    }
}
