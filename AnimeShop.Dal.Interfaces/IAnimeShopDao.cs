using AnimeShop.Common;

namespace AnimeShop.Dal.Interfaces
{
    public interface IAnimeShopDao
    {
        Common.AnimeShop getAnimeShopById(int id);
        List<Common.AnimeShop> getAllUsers();

    }
}
