using Cargohub.Models;

namespace Cargohub.Services
{
    public interface ITransferService
    {
        public List<Transfer> GetTransfers();
        public Transfer GetTransfer(int id);
        //public List<Item> GetItems(int id);
        public bool AddTransfer(Transfer transfer);
        public bool UpdateTransfer(int id, Transfer transfer);
        public bool DeleteTransfer(int id);


    }
}

