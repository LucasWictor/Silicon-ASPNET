using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class AddressEntity
    {
        [Key]
        public int Id { get; set; }
    
        public string StreetName { get; set; } = null!;
    
        public string PostalCode { get; set; } = null!;
    
        public string City { get; set; } = null!;


        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
        public string Addressline1 { get; set; }
        public string? Addressline2 { get; set; }
    }
}