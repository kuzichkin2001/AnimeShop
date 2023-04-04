using AnimeShop.Common;
using AnimeShop.Dal.DbContexts;
using AnimeShop.Dal.Interfaces;
using AnimeShop.EFDal;
using Microsoft.EntityFrameworkCore;

namespace AnimeShop.Dal;

public class ProductDao : BaseDao, IProductDao
{
    protected ProductDao(NpgsqlContext context) : base(context)
    {
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await DNpgsqlContext.Products.FirstAsync(p => p.Id == id);
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return DNpgsqlContext.Products;
    }

    public async Task CreateProductAsync(Product product)
    {
        await DNpgsqlContext.Products.AddAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await GetProductByIdAsync(id);
        DNpgsqlContext.Products.Remove(product);

        var productsCount = await DNpgsqlContext.SaveChangesAsync();
        return productsCount != 0;
    }

    public async Task UpdateProductAsync(Product product)
    {
        DNpgsqlContext.Update(product);
        await DNpgsqlContext.SaveChangesAsync();
    }
}