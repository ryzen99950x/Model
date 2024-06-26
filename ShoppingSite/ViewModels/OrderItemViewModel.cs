using ShoppingSite.Models;
namespace ShoppingSite.ViewModels;

public class OrderItemViewModel
{
    public int Id { get; set; } 
    public string ProductName { get; set; } 
    public string ProductImage { get; set; } 
    public decimal ProductPrice { get; set; } 
    public int ProductStock { get; set; } 
    public int Quantity { get; set; } 
}
