using Microsoft.EntityFrameworkCore;
using AnimeShop.Dal.DbContexts;
using AnimeShop.TelegramBot;
using AnimeShop.Common;

namespace TestPet
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var config = builder.Configuration
                .GetSection("EnvironmentVariables")
                .Get<EnvironmentVariables>();

            builder.Services.AddEntityFrameworkNpgsql().AddDbContext<NpgsqlContext>(
                options =>
                {
                    options.UseNpgsql(config?.NpgsqlConnectionString);
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            Parallel.Invoke(TelegramBotCommunications.StartPolling, app.Run);
        }
    }
}
