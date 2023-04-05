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

    public IEnumerable<Common.AnimeShop> GetAllAnimeShops()
    {
        return DNpgsqlContext.AnimeShops;
    }

    public async Task<bool> RemoveAnimeShopAsync(int id)
    {
        var animeshop = await GetAnimeShopByIdAsync(id);
        DNpgsqlContext.AnimeShops.Remove(animeshop);

        var animeshopsCount = await DNpgsqlContext.SaveChangesAsync();
        return animeshopsCount != 0;
    }

    public async Task UpdateAnimeShopAsync(Common.AnimeShop animeShop)
    {
        DNpgsqlContext.Update(animeShop);
        await DNpgsqlContext.SaveChangesAsync();
    }
}