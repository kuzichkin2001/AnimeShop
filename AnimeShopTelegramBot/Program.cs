using AnimeShop.Dal;
using AnimeShop.Dal.DbContexts;
using AnimeShop.Dal.Interfaces;
using AnimeShopBot;
using AnimeShopBot.Utils;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var generalConfiguration = hostContext.Configuration;
        services.Configure<EnvironmentVariables>(
            generalConfiguration.GetSection("EnvironmentVariables"));
        
        services.AddHostedService<Worker>();

        var npgsqlConfig = generalConfiguration
            .GetSection("EnvironmentVariables")
            .Get<EnvironmentVariables>();

        services.AddDbContext<NpgsqlContext>(options =>
        {
            options.UseNpgsql(npgsqlConfig?.NpgsqlConnectionString);
        });
        services.AddSingleton<IUserDao, UserDao>();
    })
    .Build();

await host.RunAsync();