using AnimeShop.Dal.Interfaces;

namespace AnimeShopTelegramBot;

public class MyScopedService : IScopedService
{
    private readonly ILogger<Worker> _logger;
    private readonly IUserDao _userDao;

    public MyScopedService(ILogger<Worker> logger, IUserDao userDao)
    {
        _logger = logger;
        _userDao = userDao;
    }
}

public interface IScopedService
{
}