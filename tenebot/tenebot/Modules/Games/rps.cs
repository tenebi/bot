using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace tenebot.Modules.Utility
{
    [Group("rps")]
    public class rps : ModuleBase<SocketCommandContext>
    {
        private bool IsPrivateMessage(SocketMessage msg)
        {
            return (msg.Channel.GetType() == typeof(SocketDMChannel));
        }

        [Command ("help")]
        public async Task OutputHelp()
        {
            EmbedBuilder doneBuilder = new EmbedBuilder();
            doneBuilder.WithTitle("Help for !rps command")
                .WithDescription($"usage : !rps @usertag to start the game.")
                .WithColor(Color.Orange);
        }

        [Command]
        public async Task Start(SocketGuildUser targetuser)
        {
            EmbedBuilder doneBuilder = new EmbedBuilder();
            doneBuilder.WithTitle("RPS is on!")
                .WithDescription($"{Context.User.Mention} has started a good ol' round of rock paper scissors with {targetuser}, may the best man win!")
                .WithColor(Color.Orange);

            SocketGuildUser firstuser = Context.User.Mention;
            string message1 = "Your request has been sent to " + targetuser + ", please type your choice here.";
            string message2 = $"{Context.User.Mention} has invited you to a round of rock paper scissors, please type your choice here.";

            await UserExtensions.SendMessageAsync(firstuser, message1);
            await UserExtensions.SendMessageAsync(targetuser, message2);
        }
    }
}