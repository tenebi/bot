using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using tenebot.Services;

namespace tenebot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        private Random rand = new Random();

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            Settings.Load();

            //subs
            _client.Log += Log;
            _commands.Log += Log;

            await RegisterCommandAsync();
            await _client.LoginAsync(TokenType.Bot, Settings.BotToken);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private Task Log(LogMessage message)
        {
            Debugging.Log(message);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);
                Debugging.Log("Command Handler", $"{context.User.Username} called {message}");

                var result = await _commands.ExecuteAsync(context, argPos, _services);
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
    }
}

