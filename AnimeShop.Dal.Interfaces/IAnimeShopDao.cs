using AnimeShop.Common;

namespace AnimeShop.Dal.Interfaces
{
    public interface IAnimeShopDao
    {
        Task<Common.AnimeShop?> GetAnimeShopByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsOfAnimeShopAsync(int id);
        IEnumerable<Common.AnimeShop> GetAllAnimeShopsAsync();

    }
}
