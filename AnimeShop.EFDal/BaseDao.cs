using AnimeShop.Dal.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AnimeShop.EFDal;

public class BaseDao : IAsyncDisposable, IDisposable
{
    protected readonly NpgsqlContext DNpgsqlContext;

    protected BaseDao(NpgsqlContext context)
    {
        DNpgsqlContext = context;
    }

    public async ValueTask DisposeAsync()
    {
        await DNpgsqlContext.DisposeAsync();
    }

    public void Dispose()
    {
        DNpgsqlContext.Dispose();
    }
}