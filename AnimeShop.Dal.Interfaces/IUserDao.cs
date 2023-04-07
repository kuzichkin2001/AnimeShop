using AnimeShop.Common;

namespace AnimeShop.Dal.Interfaces;

public interface IUserDao
{
    Task<User?> GetUserAsync(string login, string password);
    Task RegisterUserAsync(User user);
    Task<bool?> CheckUserCredentialsAsync(string login, string oneTimePassword);
    Task<bool?> ChangePersonalInfoAsync(User user);
}