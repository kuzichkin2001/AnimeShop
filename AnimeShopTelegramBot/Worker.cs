using AnimeShop.Dal.Interfaces;
using AnimeShopTelegramBot.Utils;
using Microsoft.Extensions.Options;

namespace AnimeShopTelegramBot;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
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