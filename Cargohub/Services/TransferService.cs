using Cargohub.Models;

namespace Cargohub.Services
{
    public class TransferService : ITransferService 
    {

        private AppDbContext data;

        public TransferService(AppDbContext _data)
        {
            data = _data;
        }

        public List<Transfer> GetTransfers()
        {
            if (data.Transfers.Count() == 0) return new List<Transfer>();
            return data.Transfers.ToList();
        }

        public Transfer? GetTransfer(int id)
        {
            //if(data.Transfers.SingleOrDefault(x => x.id == id) == null) return new Transfer()
            return data.Transfers.SingleOrDefault(x => x.id == id);
        }

        public List<Item>? GetItems(int id)
        {
            if (data.Transfers.SingleOrDefault(x => x.id == id) is null) return null;
            return data.Transfers.Where(x => x.id == id).Single().items.ToList();
        }
        public bool AddTransfer(Transfer transfer)
        {
            if (transfer is null || data.Transfers.Contains(transfer)) return false;
            data.Transfers.Add(transfer);
            data.SaveChanges();
            return true;
        }
        public bool UpdateShipment(int id, Transfer transfer)
        {
            if (transfer is null || id != transfer.id) return false;
            if (data.Transfers.SingleOrDefault(x => x.id == id) is null) return false;
            data.Transfers.Remove(data.Transfers.Where(x => x.id == id).Single());
            data.Transfers.Add(transfer);
            data.SaveChanges();
            return true;
        }
        public bool DeleteShipment(int id)
        {
            if (data.Transfers.SingleOrDefault(x => x.id == id) is null) return false;
            data.Transfers.Remove(data.Transfers.Where(x => x.id == id).Single());
            data.SaveChanges();
            return true;
        }
    }
}

