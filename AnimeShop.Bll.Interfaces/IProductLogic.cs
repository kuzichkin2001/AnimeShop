using AnimeShop.Common;

namespace AnimeShop.Bll.Interfaces;

public interface IProductLogic
{
    Task<Product> GetProductByIdAsync(int id);
    IEnumerable<Product> GetAllProducts();
    Task CreateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
    Task UpdateProductAsync(Product product);
}