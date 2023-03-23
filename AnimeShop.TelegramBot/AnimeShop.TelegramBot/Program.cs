using System;
using Telegram.Bot;

namespace AnimeShop.TelegramBot
{
    public class Program
    {
        private static void Main(string[] args)
        {
            TelegramBotCommunications.StartPolling();
        }
    }
}