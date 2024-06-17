namespace ShoppingSite.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Description { get; set; } // 商品説明
        public float Size { get; set; }         // サイズ
        public string? Image { get; set; }       // 商品画像
        public string? Name { get; set; }
        public int Price { get; set; }
        public float Weight { get; set; }       // 重量
        public string? Material { get; set; }    // 素材
        public int GenderId { get; set; }
        public int Stock { get; set; }          // 在庫数
        public int Limited { get; set; }        // オンライン限定
        public string? Package { get; set; }     // パッケージ
        public int CategoryId { get; set; }     // カテゴリーID
        public int ReviewId { get; set; }       // レビューID
        public int Sales { get; set; }          // 売上

        // Navigation properties
        public Categories? Category { get; set; }
        public Reviews? Review { get; set; }
        public Gender? Gender { get; set; }
    }
}
