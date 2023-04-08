using System.ComponentModel.DataAnnotations;
using AnimeShop.Common;

namespace TestPet.Views;

public class AnimeShopView
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string MainUrl { get; set; }
    [Required]
    public DateOnly AssortmentUpdateDate { get; set; }
    [Required]
    public IEnumerable<Product> Products { get; set; }
}