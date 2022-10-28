using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;


namespace TelegramBotASPEC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string BotToken = "token";

            var ITelegramBotClient = new TelegramBotClient(BotToken);

            var cts = new CancellationTokenSource();

            var handler = new UpdateHandler();

            var receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = { },
            };

            ITelegramBotClient.StartReceiving(handler, receiverOptions, cancellationToken: cts.Token);

            Console.ReadLine();
        }
    }
}