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
        public List<BankAccounts>? BankAccounts { get; set; }
        public List<Credits>? Credits { get; set; }
        public ICollection<Carts>? Carts { get; set; }
        public ICollection<Favorites>? Favorites { get; set; }
        [Required(ErrorMessage = "名前を入力してください")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "氏名は最小3文字から最大50文字で入力してください。")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "Emailアドレスを入力してください。")]
        [EmailAddress(ErrorMessage = "正しい形式でEmailアドレスを入力してください。")]
        public override string Email { get; set; }
        public int? BankId { get; set; }
        public int? CreditId { get; set; }

    }
}
