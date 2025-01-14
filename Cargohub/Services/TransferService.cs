using Cargohub.Models;
using Microsoft.EntityFrameworkCore;

namespace Cargohub.Services
{
    public class TransferService : ITransferService
    {

        private AppDbContext data;

        public TransferService(AppDbContext _data)
        {
            data = _data;
        }

        public async Task<List<Transfer>> GetTransfers(int amount = 100)
        {
            return await data.Transfers.Take(amount).ToListAsync();
        }

        public async Task<Transfer> GetTransfer(int id)
        {
            return await data.Transfers.FindAsync(id);
        }

        // public List<Item>? GetItems(int id)
        // {
        //     if (data.Transfers.SingleOrDefault(x => x.id == id) is null) return null;
        //     return data.Transfers.Where(x => x.id == id).Single().items.ToList();
        // }

        public async Task<Transfer> AddTransfer(Transfer transfer)
        {
            transfer.id = data.Transfers.Count() + 1;
            transfer.created_at = DateTime.UtcNow;
            transfer.updated_at = DateTime.UtcNow;

            data.Transfers.Add(transfer);
            await data.SaveChangesAsync();
            return transfer;
        }
        public async Task<bool> UpdateTransfer(Transfer transfer)
        {
            Transfer existingTransfer = await data.Transfers.FindAsync(transfer.id);
            if (existingTransfer == null)
            {
                return false;
            }

            existingTransfer.reference = transfer.reference;
            existingTransfer.transfer_from = transfer.transfer_from;
            existingTransfer.transfer_to = transfer.transfer_to;
            existingTransfer.transfer_status = transfer.transfer_status;
            //existingTransfer.items = transfer.items;
            existingTransfer.isdeleted = transfer.isdeleted;
            existingTransfer.updated_at = DateTime.UtcNow;


            data.Transfers.Update(existingTransfer);
            await data.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteTransfer(int id)
        {
            var transfer = await data.Transfers.FindAsync(id);
            if (transfer == null)
            {
                return false;
            }

            data.Transfers.Remove(transfer);
            await data.SaveChangesAsync();
            return true;
        }
    }
}

