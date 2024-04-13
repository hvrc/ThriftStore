using System.ComponentModel.DataAnnotations;

namespace ThriftStore.Models
{
    public class Listing
    {
        public int ListingID { get; set; }
        [Required]
        public int TempUserID { get; set; } 
        [Required]
        public string Address { get; set; } = "";
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public bool IsAvailable { get; set; }
    }
}
