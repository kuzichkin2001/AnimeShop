namespace AnimeShop.TelegramBot;

public class Program
{
    private static async Task Main(string[] args)
    {
        TelegramBotCommunications.StartPolling();
    }
}