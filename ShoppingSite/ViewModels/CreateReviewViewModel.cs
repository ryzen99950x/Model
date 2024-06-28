using ShoppingSite.Models;
namespace ShoppingSite.ViewModels;
using System.ComponentModel.DataAnnotations;
    public class CreateReviewViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "レビューを入力してください。")]
        public string Comment { get; set; }

        [Range(1, 5, ErrorMessage = "星の評価は1から5の間で選択してください。")]
        public int Rating { get; set; }
    }
