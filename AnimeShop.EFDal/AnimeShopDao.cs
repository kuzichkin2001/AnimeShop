using AnimeShop.Common;
using AnimeShop.Dal.DbContexts;
using AnimeShop.Dal.Interfaces;
using AnimeShop.EFDal;
using Microsoft.EntityFrameworkCore;

namespace AnimeShop.Dal;

public class AnimeShopDao : BaseDao, IAnimeShopDao
{
    public AnimeShopDao(NpgsqlContext context)
        : base(context)
    {
    }

    public async Task<Common.AnimeShop?> GetAnimeShopByIdAsync(int id)
    {
        return await DNpgsqlContext.AnimeShops.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProductsOfAnimeShopAsync(int id)
    {
        var animeShop = await DNpgsqlContext.AnimeShops.FirstAsync(a => a.Id == id);

        return animeShop.Products;
    }

    public IEnumerable<Common.AnimeShop> GetAllAnimeShopsAsync()
    {
        return DNpgsqlContext.AnimeShops;
    }
}