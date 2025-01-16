using Cargohub.Models;
using Microsoft.EntityFrameworkCore;

namespace Cargohub.Services
{
    public class TransferService : ITransferService
    {

        private AppDbContext _context;

        public TransferService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transfer>> GetTransfers(int amount = 100)
        {
            return await _context.Transfers.Take(amount).ToListAsync();
        }

        public async Task<Transfer> GetTransfer(int id)
        {
            return await _context.Transfers.FindAsync(id);
        }

        public async Task<Transfer> AddTransfer(Transfer NewTransfer)
        {
            Transfer transfer = new Transfer
            {
                reference = NewTransfer.reference,
                transfer_from = NewTransfer.transfer_from,
                transfer_to = NewTransfer.transfer_to,
                transfer_status = NewTransfer.transfer_status,
            };
            _context.Transfers.Add(transfer);
            await _context.SaveChangesAsync();
            return transfer;
        } 
        public async Task<bool> UpdateTransfer(Transfer transfer)
        {
            Transfer existingTransfer = await _context.Transfers.FindAsync(transfer.id);
            if (existingTransfer == null)
            {
                return false;
            }

            existingTransfer.reference = transfer.reference;
            existingTransfer.transfer_from = transfer.transfer_from;
            existingTransfer.transfer_to = transfer.transfer_to;
            existingTransfer.transfer_status = transfer.transfer_status;
            existingTransfer.isdeleted = transfer.isdeleted;
            existingTransfer.updated_at = DateTime.UtcNow;


            _context.Transfers.Update(existingTransfer);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteTransfer(int id)
        {
            var transfer = await _context.Transfers.FindAsync(id);
            if (transfer?.isdeleted == true || transfer == null)
            {
                return false;
            }

            transfer.isdeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

