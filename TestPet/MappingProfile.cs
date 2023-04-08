using AutoMapper;
using AnimeShop.Common;
using TestPet.Views;

namespace TestPet;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserCredentialsView, User>();
        CreateMap<ProductView, Product>();
        CreateMap<AnimeShopView, AnimeShop.Common.AnimeShop>();
    }
}