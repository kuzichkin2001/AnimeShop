using AnimeShop.Common;

namespace AnimeShop.Bll.Interfaces;

public interface IAnimeShopLogic
{
    Task<Common.AnimeShop?> GetAnimeShopByIdAsync(int id);
    Task<IEnumerable<Product>> GetProductsOfAnimeShopsAsync(int id);
    IEnumerable<Common.AnimeShop> GetAllAnimeShops();
    Task<bool> RemoveAnimeShopAsync(int id);
    Task UpdateAnimeShopAsync(Common.AnimeShop animeShop);
}