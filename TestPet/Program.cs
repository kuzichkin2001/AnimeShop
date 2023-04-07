using System.Text.Json.Serialization;
using AnimeShop.Bll;
using AnimeShop.Bll.Interfaces;
using Microsoft.EntityFrameworkCore;
using AnimeShop.Dal.DbContexts;
using AnimeShop.TelegramBot;
using AnimeShop.Common;
using AnimeShop.Dal;
using AnimeShop.Dal.Interfaces;
using AutoMapper;

namespace TestPet
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLogging();
            
            builder.Services.AddScoped<IUserDao, UserDao>();
            builder.Services.AddScoped<IAnimeShopDao, AnimeShopDao>();
            builder.Services.AddScoped<IProductDao, ProductDao>();
            builder.Services.AddScoped<IUserLogic, UserLogic>();
            builder.Services.AddScoped<IAnimeShopLogic, AnimeShopLogic>();
            builder.Services.AddScoped<IProductLogic, ProductLogic>();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                var jsonConverter = new JsonStringEnumConverter();
                options.JsonSerializerOptions.Converters.Add(jsonConverter);
            });

            var config = builder.Configuration
                .GetSection("EnvironmentVariables")
                .Get<EnvironmentVariables>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                var mappingProfile = new MappingProfile();
                mc.AddProfile(mappingProfile);
            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddDbContext<NpgsqlContext>(
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

            var options = new DbContextOptionsBuilder<NpgsqlContext>();
            options.UseNpgsql(config?.NpgsqlConnectionString);
            
            app.Run();
        }
    }
}
