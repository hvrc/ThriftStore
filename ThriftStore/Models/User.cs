using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThriftStore.Models;

namespace ThriftStore.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string FirstName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        public List<Listing> Listings { get; set; } = new List<Listing>();
    }
}
