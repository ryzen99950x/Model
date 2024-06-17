namespace ShoppingSite.Models
{
    public class Payments
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public int CreditId {  get; set; }
        public int UserId { get; set; }
    }
}
