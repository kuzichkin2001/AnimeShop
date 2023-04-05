using Microsoft.Extensions.Logging;

namespace AnimeShop.Bll
{
    public class BaseLogic
    {
        protected readonly ILogger Logger;

        public BaseLogic(ILogger<BaseLogic> logger)
        {
            Logger = logger;
        }
    }
}
