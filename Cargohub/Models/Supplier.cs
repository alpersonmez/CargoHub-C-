using Microsoft.AspNetCore.Http.Features;
namespace Cargohub.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AddressExtra { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string Reference { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool isSoftDeleted { get; set; }
    }
}

