using AnimeShop.Common;

namespace AnimeShop.Bll.Interfaces
{
    public interface IUserLogic
    {
        Task<User?> GetUserAsync(string login, string password);
        Task RegisterUserAsync(User user);
        Task<bool> CheckUserCredentialsAsync(string login, string oneTimePassword);
        Task<bool?> ChangePersonalInfoAsync(User user);
    }
}
