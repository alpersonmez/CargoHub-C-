using Cargohub.Models;

namespace Cargohub.Services
{
    public interface ITransferService
    {
        public Task<List<Transfer>> GetTransfers(int amount = 100);
        public Task<Transfer> GetTransfer(int id);
        //public List<Item> GetItems(int id);
        public Task<Transfer> AddTransfer(Transfer transfer);
        public Task<bool> UpdateTransfer(Transfer transfer);
        public Task<bool> DeleteTransfer(int id);


    }
}

