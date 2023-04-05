using AnimeShop.Bll.Interfaces;
using AnimeShop.Common;
using AnimeShop.Dal.Interfaces;
using Microsoft.Extensions.Logging;

namespace AnimeShop.Bll;

public class ProductLogic : BaseLogic, IProductLogic
{
    private readonly IProductDao _productDao;
    
    public ProductLogic(ILogger<ProductLogic> logger, IProductDao productDao) : base(logger)
    {
        _productDao = productDao;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _productDao.GetProductByIdAsync(id);
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _productDao.GetAllProducts();
    }

    public async Task CreateProductAsync(Product product)
    {
        await _productDao.CreateProductAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _productDao.DeleteProductAsync(id);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productDao.UpdateProductAsync(product);
    }
}