using AnimeShop.Dal.Interfaces;
using AnimeShopTelegramBot.Utils;
using Microsoft.Extensions.Options;

namespace AnimeShopTelegramBot;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Worker> _logger;
    private readonly IOptions<EnvironmentVariables> _options;

    public Worker(
        IServiceProvider serviceProvider,
        ILogger<Worker> logger,
        IOptions<EnvironmentVariables> options)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _options = options;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var userDao = scope.ServiceProvider.GetRequiredService<IUserDao>();
            var options = scope.ServiceProvider.GetRequiredService<IOptions<EnvironmentVariables>>();
            
            var telegramBot = new TelegramBotCommunications(userDao, options);
        
            telegramBot.StartPolling();
        }
        
        
        return Task.CompletedTask;
    }
}