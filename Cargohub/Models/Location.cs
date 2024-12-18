namespace Cargohub.Models
{
    public class Location
    {
        public int id { get; set; }
        public int warehouse_id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime update_at { get; set; }
    }
}