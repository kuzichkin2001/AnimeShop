namespace AnimeShopTelegramBot;

public class MyScopedService : IScopedService
{
    private IServiceProvider _serviceProvider;

    public MyScopedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IServiceProvider ServiceProvider
    {
        get
        {
            return _serviceProvider;
        }
    }
}

public interface IScopedService
{
    IServiceProvider ServiceProvider { get; }
}