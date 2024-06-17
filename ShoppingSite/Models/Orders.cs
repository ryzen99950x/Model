using ShoppingSite.Models;

namespace ShoppingSite.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public Carts? Cart { get; set; }
    }
}
