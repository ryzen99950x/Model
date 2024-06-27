using ShoppingSite.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoppingSite.Models
{
    public class Credits
    {
        public int Id { get; set; }
        [StringLength(8, MinimumLength = 8, ErrorMessage = "下8桁は8桁で入力してください。")]
        public string? CardNum1 { get; set; }
        [StringLength(8, MinimumLength = 8, ErrorMessage = "上8桁は8桁で入力してください。")]
        public string? CardNum2 { get; set; }
        public int SecurityCode { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
