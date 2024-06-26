using ShoppingSite.Models;

namespace ShoppingSite.Models
{
    public class Credits
    {
        public int Id { get; set; }
        public int CardNum1 { get; set; }
        public int CardNum2 { get; set; }
        public int SecurityCode { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
