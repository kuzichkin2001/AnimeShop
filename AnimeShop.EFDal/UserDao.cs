using System.Security.Cryptography;
using System.Text;
using AnimeShop.Common;
using AnimeShop.Common.Utils;
using AnimeShop.Dal.DbContexts;
using AnimeShop.Dal.Interfaces;
using AnimeShop.EFDal;
using Microsoft.EntityFrameworkCore;

namespace AnimeShop.Dal;

public class UserDao : BaseDao, IUserDao
{
    private const string SALT = "cust0ms4ltt000ld";
    public UserDao(NpgsqlContext context) : base(context)
    {
    }

    public IEnumerable<User> GetUsers()
    {
        return DNpgsqlContext.Users;
    }

    public async Task<User?> GetUserAsync(string login, string password)
    {
        return await DNpgsqlContext.Users
            .FirstOrDefaultAsync(u => u.Email == login && u.Password == password);
    }

    public async Task RegisterUserAsync(User user)
    {
        user.Password = Utilities.ComputeHashForPassword(user.Password);
        user.AccountActivated = true;
        
        await DNpgsqlContext.Users.AddAsync(user);
        await DNpgsqlContext.SaveChangesAsync();
    }

    public async Task<bool> CheckUserCredentialsAsync(string login, string oneTimePassword)
    {
        var user = await DNpgsqlContext.Users.FirstAsync(u => u.Email == login);

        return user.Password.Equals(oneTimePassword);
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

    public User? GetNonactivatedUser(string login)
    {
        return DNpgsqlContext.Users.FirstOrDefault(u => u.Email == login && !u.AccountActivated);
    }

    public void ClearAllWithSameChatId(long chatId)
    {
        var users = GetUsers();

        var filteredUsers = users.Where(u => u.ChatId == chatId);

        foreach (var user in filteredUsers)
        {
            user.ChatId = 0;
            ChangePersonalInfoAsync(user);
        }
    }
}