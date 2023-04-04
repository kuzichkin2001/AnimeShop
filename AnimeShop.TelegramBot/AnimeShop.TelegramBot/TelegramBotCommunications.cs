using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace AnimeShop.TelegramBot
{
    public static class TelegramBotCommunications
    {
        private static TelegramBotClient _tgBot;
        private static InlineKeyboardMarkup Keyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Сгенерировать пароль", "/getpassword")
            }
        });

        static TelegramBotCommunications()
        {
            string folder = Directory.GetCurrentDirectory();
            Console.WriteLine(folder);
            DirectoryInfo dir = new(folder);
            List<FileInfo> projectRootDir = dir.Parent.Parent.Parent.GetFiles().ToList();
            string environ = projectRootDir.Find(f => f.Name == ".env").FullName;

            using (StreamReader sr = new(environ, Encoding.UTF8))
            {
                string[] fileData = sr
                    .ReadToEnd()
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries);

                Dictionary<string, string> environmentData = new();

                foreach (var dataString in fileData)
                {
                    string[] keyAndValue = dataString.Split('=');
                    environmentData.Add(keyAndValue[0], keyAndValue[1]);
                }

                _tgBot = new TelegramBotClient(environmentData["TOKEN"]);
            }
        }

        public static void StartPolling()
        {
            _tgBot.StartReceiving(Update, Error);

            Console.ReadLine();
        }

        private static async Task Update(ITelegramBotClient tgClient, Update update, CancellationToken token)
        {
            var message = update.Message.Text;
            var chatId = update.Message.Chat.Id;
            Random rnd = new Random();

            switch (message)
            {
                case "/start":
                    await tgClient.SendTextMessageAsync(chatId, "Добро пожаловать!", replyMarkup: Keyboard, cancellationToken: token);
                    break;
                case "/getpassword":
                    byte[] bytes = new byte[25];
                    rnd.NextBytes(bytes);
                    byte[] randomPassword = bytes.Select(b => (byte)(b % 26 + 97)).ToArray();
                    string resultPassword = "";

                    foreach (var c in randomPassword)
                    {
                        bool rndUpper = rnd.Next(0, 2) == 1;
                        resultPassword += rndUpper ? (char) (c - 32) : (char) c;
                    }
                    
                    await tgClient.SendTextMessageAsync(update.Message.Chat.Id, resultPassword, replyMarkup: Keyboard, cancellationToken: token);
                    break;
                default:
                    await tgClient.SendTextMessageAsync(update.Message.Chat.Id, "Ты че, идиот?");
                    break;
            }
        }

        private static async Task Error(ITelegramBotClient tgClient, Exception ex, CancellationToken token)
        {
            bool result = await tgClient.TestApiAsync(token);
            if (!result)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}