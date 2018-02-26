using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace tenebot.Modules.Utility
{
    [Group("rps")]
    public class Rps : ModuleBase<SocketCommandContext>
    {
        private class RpsPlayers
        {
            private SocketUser firstPlayer;
            private SocketUser secondPlayer;

            public SocketUser SecondPlayer { get => secondPlayer; set => secondPlayer = value; }
            public SocketUser FirstPlayer { get => firstPlayer; set => firstPlayer = value; }

            public RpsPlayers()
            {
                firstPlayer = null;
                secondPlayer = null;
            }

            public bool PlayersReady => firstPlayer != null && secondPlayer != null ? true : false;
        }

        private static System.Timers.Timer timer = new System.Timers.Timer();
        private static RpsPlayers players = new RpsPlayers();

        private bool IsPrivateMessage(SocketMessage msg)
        {
            return (msg.Channel.GetType() == typeof(SocketDMChannel));
        }

        private void HandleTimer(object sender, ElapsedEventArgs e)
        {
            EmbedBuilder msg = new EmbedBuilder()
                .WithTitle(":scissors: :newspaper: :gem: The brawl has timed out")
                .WithColor(Color.Magenta);

            //await ReplyAsync("", false, messages[0].Build());

            timer.Stop();
        }

        private EmbedBuilder[] BuildMessages(SocketUser firstPlayer, SocketUser secondPlayer)
        {
            EmbedBuilder firstPlayerMessage = new EmbedBuilder();
            firstPlayerMessage.WithTitle("RPS is on!")
                .WithDescription($"You've started a rock paper scissors match with {secondPlayer}.")
                .WithColor(Color.Blue);

            EmbedBuilder secondPlayerMessage = new EmbedBuilder();
            secondPlayerMessage.WithTitle("RPS is on!")
                .WithDescription($"{Context.User.Mention} challanged you to rock paper scissors.\nWrite your selection or 'withdraw' to pussy out.")
                .WithColor(Color.Red);

            EmbedBuilder channelMessage = new EmbedBuilder()
                .WithTitle(":scissors: :newspaper: :gem: A brawl is surely brewing.")
                .WithDescription($"{firstPlayer.Mention} challanged {secondPlayer.Mention} to rock paper scissors!")
                .WithColor(Color.Magenta);

            return new EmbedBuilder[] { firstPlayerMessage, secondPlayerMessage, channelMessage };
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
        public async Task Start(SocketUser secondPlayer)
        {
            SocketUser firstPlayer = Context.User;

            EmbedBuilder[] messages = BuildMessages(firstPlayer, secondPlayer);

            await ReplyAsync("", false, messages[0].Build());
            await UserExtensions.SendMessageAsync(firstPlayer, "", false, messages[1]);
            await UserExtensions.SendMessageAsync(secondPlayer, "", false, messages[2]);

            players.FirstPlayer = firstPlayer;
            players.SecondPlayer = secondPlayer;

            timer.Interval = 120000;
            timer.Elapsed += new ElapsedEventHandler(HandleTimer);
            timer.Start();
        }
    }
}