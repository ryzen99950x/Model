using ShoppingSite.Models;
using System.ComponentModel;

namespace ShoppingSite.Models
{
    public class Carts
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        [DefaultValue(1)]
        public int DupCount { get; set; } = 1;
        public User? User { get; set; }
        public Products? Product { get; set; }
        public List<Carts> OrderItems { get; set; }
    }
}