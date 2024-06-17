using ShoppingSite.Models;

namespace ShoppingSite.Models
{
    public class Favorites
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int OderId { get; set; }
        public User? User { get; set; }
        public Orders? Oder { get; set; }
        public Product? Product { get; set; }
    }
}
