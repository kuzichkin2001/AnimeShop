using AnimeShop.Common;
using AnimeShop.Dal.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using EnvironmentVariables = AnimeShopTelegramBot.Utils.EnvironmentVariables;
using AnimeShopUser = AnimeShop.Common.User;

namespace AnimeShopTelegramBot
{
    public class TelegramBotCommunications
    {
        private readonly TelegramBotClient _tgBot;
        private readonly IUserDao _userDao;
        private readonly ILogger<TelegramBotCommunications> _logger;
        
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
            
            var token = options.Value.TelegramBotToken;
            _tgBot = new TelegramBotClient(token);
        }

        private AnimeShopUser? GetUserByLogin(string login)
        {
            var user = _userDao.GetNonactivatedUser(login);

            if (user == null)
            {
                _logger.LogError("There's no non-activated user with such login {Login}", login);
                return null;
            }

            return user;
        }

        private AnimeShopUser? GetUserCredentials(long chatId)
        {
            var user = _userDao.GetUserByChatId(chatId);

            if (user == null)
            {
                _logger.LogWarning("You should bind a user with Telegram chat {ChatId}. " +
                                   "You should be signed up in an application via Telegram Bot", chatId);

                return null;
            }
            
            _logger.LogInformation("User credentials from chat {ChatId} were successfully got", chatId);
            return user;
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

            var user = GetUserCredentials(chatId);

            if (user != null && !user.AccountActivated)
            {
                switch (message)
                {
                    case "/getPassword":
                        var resultPassword = "";
                        
                        var bytes = new byte[25];
                        rnd.NextBytes(bytes);
                        
                        foreach (var c in bytes)
                        {
                            resultPassword += (char)(c % 26 + 94);
                        }
                        
                        tgClient.SendTextMessageAsync(chatId, resultPassword, cancellationToken: token);

                        user.Password = resultPassword;
                        user.AccountActivated = true;
                        _userDao.ChangePersonalInfoAsync(user);

                        break;
                    case "/getLogin":
                        _logger.LogInformation("Trying to get login for user of chat: {ChatId}", chatId);
                        tgClient.SendTextMessageAsync(chatId, "Введите логин:", cancellationToken: token);
                        
                        PreviousMessage = "/getLogin";
                        break;
                    default:
                        if (PreviousMessage == "/getLogin")
                        {
                            user = GetUserByLogin(message);
                            _userDao.ClearAllWithSameChatId(chatId);
                            user.ChatId = chatId;

                            tgClient.SendTextMessageAsync(
                                chatId,
                                "Сгенерируйте пароль при помощи команды /getPassword",
                                cancellationToken: token);
                        }
                        
                        PreviousMessage = message;
                        break;
                }
            }
            else if (user != null && user.AccountActivated)
            {
                tgClient.SendTextMessageAsync(chatId, "Ваш аккаунт распознан.",
                    cancellationToken: token);
            }
            else if (user == null)
            {
                
                tgClient.SendTextMessageAsync(chatId, "Введите логин с помощью /getLogin", cancellationToken: token);
                PreviousMessage = "/getLogin";
            }
        }

        private void Error(
            ITelegramBotClient tgClient,
            Exception ex,
            CancellationToken token)
        {
            _logger.LogError(
                "{ExceptionType}: There's some error. {ExceptionMessage}",
                ex.GetType(),
                ex.Message);
        }
    }
}