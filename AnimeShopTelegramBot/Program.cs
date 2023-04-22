using AnimeShopTelegramBot;
using System.Collections.Immutable;
using AnimeShop.Dal;
using AnimeShop.Dal.DbContexts;
using AnimeShop.Dal.Interfaces;
using AnimeShopTelegramBot.Utils;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateDefaultBuilder(args);
var host = builder.ConfigureServices((hostContext, services) =>
    {
        services.AddLogging();

        var generalConfiguration = hostContext.Configuration;
        services.Configure<EnvironmentVariables>(generalConfiguration.GetSection("EnvironmentVariables"));

        var npgsqlConfig = generalConfiguration
            .GetSection("EnvironmentVariables")
            .Get<EnvironmentVariables>();

        services.AddDbContext<NpgsqlContext>(options =>
        {
            options.UseNpgsql(npgsqlConfig?.NpgsqlConnectionString);
        });

        var options = new DbContextOptionsBuilder<NpgsqlContext>();
        options.UseNpgsql(npgsqlConfig?.NpgsqlConnectionString);

        services.AddScoped<IScopedService, MyScopedService>();
        services.AddScoped<IUserDao, UserDao>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
