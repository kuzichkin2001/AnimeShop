using AnimeShop.Common;

namespace AnimeShop.Dal.Interfaces;

public interface IProductDao
{
    Task<Product> GetProductByIdAsync(int id);
    IEnumerable<Product> GetAllProducts();
    Task CreateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
    Task UpdateProductAsync(Product product);
}