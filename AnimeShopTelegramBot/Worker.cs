using AnimeShop.Dal.Interfaces;
using AnimeShopBot.Utils;
using Microsoft.Extensions.Options;

namespace AnimeShopBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IUserDao _userDao;
    private readonly IOptions<EnvironmentVariables> _options;

    public Worker(ILogger<Worker> logger, IUserDao userDao, IOptions<EnvironmentVariables> options)
    {
        _logger = logger;
        _userDao = userDao;
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var telegramBot = new TelegramBotCommunications(_userDao, _options);
        
        _logger.LogInformation("Telegram Bot started working");
        telegramBot.StartPolling();
    }
}