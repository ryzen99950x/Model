namespace ShoppingSite.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public Products? Product { get; set; }
    }
}
