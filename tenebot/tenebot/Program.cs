using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using tenebot.Services;
using tenebot.Services.AdministrationServices;

namespace tenebot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();


        private Random rand = new Random();

        public async Task RunBotAsync()
        {



            Settings._client = new DiscordSocketClient(Settings._config);
            Settings._commands = new CommandService();

            Settings._services = new ServiceCollection()
                .AddSingleton(Settings._client)
                .AddSingleton(Settings._commands)
                .BuildServiceProvider();

            Settings.Load();
            Embeds.InitializeAdminEmbeds();

            //subs
            Settings._client.Log += Debugging.Log;
            Settings._commands.Log += Debugging.Log;
            Settings._client.MessageUpdated += MessageUpdated;


            await RegisterCommandAsync();
            await Settings._client.LoginAsync(TokenType.Bot, Settings.BotToken);
            await Settings._client.StartAsync();
            await Task.Delay(-1);
        }


        public async Task RegisterCommandAsync()
        {
            Settings._client.MessageReceived += HandleCommandAsync;

            await Settings._commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(Settings._client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(Settings._client, message);
                Debugging.Log("Command Handler", $"{context.User.Username} called {message}");

                var result = await Settings._commands.ExecuteAsync(context, argPos, Settings._services);
                if (!result.IsSuccess && result.Error != CommandError.ObjectNotFound || result.Error != CommandError.Exception)
                {
                    Debugging.Log("Command Handler", $"Error with command {message}: {result.ErrorReason.Replace(".", "")}", LogSeverity.Warning);
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithTitle(":no_entry_sign:  Error!")
                        .WithColor(Color.DarkRed)
                        .WithDescription(result.ErrorReason);
                    await arg.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var message = await before.GetOrDownloadAsync();
        }
    }
}

