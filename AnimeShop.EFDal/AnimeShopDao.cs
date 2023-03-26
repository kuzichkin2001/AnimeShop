using AnimeShop.Dal.DbContexts;
using AnimeShop.Dal.Interfaces;

namespace AnimeShop.EFDal
{
    public class AnimeShopDao : BaseDao, IAnimeShopDao
    {
        public AnimeShopDao(NpgsqlContext context)
            : base(context)
        {
            
        }
        
        public Common.AnimeShop getAnimeShopById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Common.AnimeShop> getAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
