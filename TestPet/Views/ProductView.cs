using System.ComponentModel.DataAnnotations;
using AnimeShop.Common;

namespace TestPet.Views;

public class ProductView
{
    public int Id { get; set; }
    
    [Required]
    public ProductType ProductType { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public bool Seasonal { get; set; }
}