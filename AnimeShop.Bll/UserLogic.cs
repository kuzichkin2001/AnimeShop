using AnimeShop.Bll.Interfaces;
using AnimeShop.Common;
using AnimeShop.Dal.Interfaces;
using Microsoft.Extensions.Logging;

namespace AnimeShop.Bll;

public class UserLogic : BaseLogic, IUserLogic
{
    private readonly IUserDao _userDao;
    
    public UserLogic(ILogger<UserLogic> logger, IUserDao userDao) : base(logger)
    {
        _userDao = userDao;
    }

    public async Task<User?> GetUserAsync(string login, string password)
    {
        return await _userDao.GetUserAsync(login, password);
    }

    public async Task RegisterUserAsync(User user)
    {
        await _userDao.RegisterUserAsync(user);
    }

    public async Task<bool?> CheckUserCredentialsAsync(string login, string oneTimePassword)
    {
        return await _userDao.CheckUserCredentialsAsync(login, oneTimePassword);
    }
}