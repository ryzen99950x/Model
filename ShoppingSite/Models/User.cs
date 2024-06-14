using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace ShoppingSite.Models
{
    public class User :IdentityUser<int>
    {
        public string Address { get; set; }
        public int RviewId { get; set; }
        public int PaymentId {  get; set; }
        public int FavoriteId { get; set; }

    }
}
