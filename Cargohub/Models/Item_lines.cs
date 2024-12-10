namespace Cargohub.Models
{
    public class Item_lines
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool isSoftDeleted { get; set; }
    }
}