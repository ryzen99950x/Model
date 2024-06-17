using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ShoppingSite.Models
{
    public class User : IdentityUser<int>
    {
        public string? Address { get; set; }

        // Navigation properties for related entities
        public List<Reviews>? Reviews { get; set; }
        public List<Payments>? Payments { get; set; }
        public List<Favorites>? Favorites { get; set; }
        public List<Orders>? Orders { get; set; }
        public List<BankAccounts>? BankAccounts { get; set; }
        public List<Credits>? Credits { get; set; }
    }
}
