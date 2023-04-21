using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnimeShop.Common;
using AnimeShop.Dal.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using EnvironmentVariables = AnimeShopBot.Utils.EnvironmentVariables;

namespace AnimeShopBot
{
    public class TelegramBotCommunications
    {
        private readonly TelegramBotClient _tgBot;
        private readonly IUserDao _userDao;
        private readonly ILogger<TelegramBotCommunications> _logger;
        private readonly IDictionary<string, string> _credentials;
        
        private string PreviousMessage { get; set; }

        private const string SALT = "cust0ms4ltt000ld";

        public TelegramBotCommunications(
            IUserDao userDao,
            IOptions<EnvironmentVariables> options)
        {
            _userDao = userDao;
            _logger = new Logger<TelegramBotCommunications>(LoggerFactory.Create(loggerBuilder =>
            {
                loggerBuilder.SetMinimumLevel(LogLevel.Trace).AddConsole();
            }));

            _credentials = new Dictionary<string, string>();
            
            var token = options.Value.TelegramBotToken;
            _tgBot = new TelegramBotClient(token);
        }

        private void GetUserCredentials(long chatId)
        {
            var user = _userDao.GetUserByChatId(chatId);

            if (user == null)
            {
                _logger.LogWarning("You should bind a user with Telegram chat {ChatId}. " +
                                   "You should be signed up in an application via Telegram Bot", chatId);
                throw new NullReferenceException("Сперва необходимо создать пользователя через бота.");
            }
            _credentials.Add("login", user.Email);
            _credentials.Add("password", user.Password);
            
            _logger.LogInformation("User credentials from chat {ChatId} were successfully got", chatId);
        }

        public void StartPolling()
        {
            PreviousMessage = "/start";
            _tgBot.StartReceiving(Update, Error);

            Console.ReadLine();
        }

        private void Update(
            ITelegramBotClient tgClient,
            Update update,
            CancellationToken token)
        {
            var message = update.Message.Text;
            var chatId = update.Message.Chat.Id;
            var rnd = new Random();
            
            GetUserCredentials(chatId);

            string login;
            string password;
            if (_credentials.ContainsKey("login") && _credentials.ContainsKey("password"))
            {
                login = _credentials["login"];
                password = _credentials["password"];
            }
            else
            {
                login = "";
                password = "";
            }
            
            
            switch (message)
            {
                case "/start":
                    _logger.LogInformation("Starting working with bot, user message: {Message}", message);
                    tgClient.SendTextMessageAsync(chatId, "Добро пожаловать!", cancellationToken: token);
                    
                    PreviousMessage = "/start";
                    break;
                case "/getLogin":
                    _logger.LogInformation("Trying to get login for user of chat: {ChatId}", chatId);
                    tgClient.SendTextMessageAsync(chatId, "Введите логин:", cancellationToken: token);
                    
                    PreviousMessage = "/getLogin";
                    break;
                case "/getPassword":
                    var bytes = new byte[25];
                    rnd.NextBytes(bytes);
                    var resultPassword = "";

                    if (_credentials.ContainsKey("login"))
                    {
                        var hash = SHA256.Create(message + SALT);
                        var rPassword = hash.Hash;

                        foreach (var c in rPassword)
                        {
                            bool rndUpper = rnd.Next(0, 2) == 1;
                            resultPassword += rndUpper ? (char) (c - 32) : (char) c;
                        }
                        
                        tgClient.SendTextMessageAsync(chatId, resultPassword, cancellationToken: token);
                    }
                    else
                    {
                        tgClient.SendTextMessageAsync(chatId, "Необходимо сначала ввести логин",
                            cancellationToken: token);
                    }
                    
                    PreviousMessage = "/getPassword";
                    break;
                default:
                    if (PreviousMessage == "/getLogin")
                    {
                        _credentials["login"] = message;
                        tgClient.SendTextMessageAsync(
                            chatId,
                            "Сгенерируйте пароль при помощи команды /getPassword",
                            cancellationToken: token);
                    }

                    PreviousMessage = message;
                    break;
            }
        }

        private void Error(
            ITelegramBotClient tgClient,
            Exception ex,
            CancellationToken token)
        {
            switch (PreviousMessage)
            {
                _logger.LogError("{ExceptionType}: There's some error", ex.GetType());
            }
        }
    }
}