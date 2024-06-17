using ShoppingSite.Models;

namespace ShoppingSite.Models
{
    public class Carts
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
