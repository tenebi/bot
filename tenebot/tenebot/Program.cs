using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

            string botToken = "NDE1OTQxODc2NjgwNDkxMDE5.DXCd-g.EEXbGoRa41HE5vtkq2jHLoT0Yp4";

            //subs
            _client.Log += Log;
            _commands.Log += Log;


            await RegisterCommandAsync();
            await _client.LoginAsync(TokenType.Bot, botToken);
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

                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess && result.Error != CommandError.ObjectNotFound || result.Error != CommandError.Exception)
                {
                    await arg.Channel.SendMessageAsync(result.ErrorReason);
                }

            }
        }
    }
}

