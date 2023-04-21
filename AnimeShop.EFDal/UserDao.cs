using AnimeShop.Common;
using AnimeShop.Dal.DbContexts;
using AnimeShop.Dal.Interfaces;
using AnimeShop.EFDal;
using Microsoft.EntityFrameworkCore;

namespace AnimeShop.Dal;

public class UserDao : BaseDao, IUserDao
{
    public UserDao(NpgsqlContext context) : base(context)
    {
    }

    public async Task<User?> GetUserAsync(string login, string password)
    {
        return await DNpgsqlContext.Users
            .FirstOrDefaultAsync(u => u.Email == login && u.Password == password);
    }

    public async Task RegisterUserAsync(User user)
    {
        await DNpgsqlContext.Users.AddAsync(user);
        await DNpgsqlContext.SaveChangesAsync();
    }

    public async Task<bool?> CheckUserCredentialsAsync(string login, string oneTimePassword)
    {
        var user = await DNpgsqlContext.Users.FirstOrDefaultAsync(u => u.Email == login);

        return user?.Password.Equals(oneTimePassword);
    }

    public async Task<bool?> ChangePersonalInfoAsync(User user)
    {
        DNpgsqlContext.Update(user);
        var userCount = await DNpgsqlContext.SaveChangesAsync();

        return userCount != 0;
    }

    public User? GetUserByChatId(long tgChatId)
    {
        return DNpgsqlContext.Users.FirstOrDefault(u => u.ChatId == tgChatId);
    }
}