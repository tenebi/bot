using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Timers;
using tenebot.Services;

namespace tenebot.Modules.Utility
{
    [Group("rps")]
    public class Rps : ModuleBase<SocketCommandContext>
    {
        enum RockPaperScissors { rock, paper, scissors, nothing };

        private class RpsPlayers
        {
            private SocketUser firstPlayer;
            private SocketUser secondPlayer;
            private RockPaperScissors firstPlayerSelection;
            private RockPaperScissors secondPlayerSelection;

            public SocketUser SecondPlayer { get => secondPlayer; set => secondPlayer = value; }
            public SocketUser FirstPlayer { get => firstPlayer; set => firstPlayer = value; }
            public RockPaperScissors FirstPlayerSelection { get => firstPlayerSelection; set => firstPlayerSelection = value; }
            public RockPaperScissors SecondPlayerSelection { get => secondPlayerSelection; set => secondPlayerSelection = value; }

            public RpsPlayers()
            {
                firstPlayer = null;
                secondPlayer = null;
                firstPlayerSelection = RockPaperScissors.nothing;
                secondPlayerSelection = RockPaperScissors.nothing;
            }

            public bool PlayersReady => firstPlayer != null && secondPlayer != null ? true : false;
        }

        private static System.Timers.Timer timer = new System.Timers.Timer();
        private static RpsPlayers players = new RpsPlayers();
        private static bool gameRunning = false;

        private bool IsPrivateMessage(SocketMessage msg)
        {
            return (msg.Channel.GetType() == typeof(SocketDMChannel));
        }

        private void StopGame()
        {
            EmbedBuilder message = new EmbedBuilder()
                .WithTitle(":scissors: :newspaper: :gem: The brawl ended before it began, someone pussied out")
                .WithColor(Color.Magenta);

            MessageHandler handler = new MessageHandler();
            handler.SendMessage(message);
            gameRunning = false;
            timer.Stop();
            players = new RpsPlayers();
        }

        private void Brawl()
        {
            EmbedBuilder message = new EmbedBuilder()
                .WithTitle("shit works")
                .WithDescription($"{players.FirstPlayer}: {players.FirstPlayerSelection} vs {players.SecondPlayerSelection} :{players.SecondPlayer}")
                .WithColor(Color.Magenta);

            MessageHandler handler = new MessageHandler();
            handler.SendMessage(message);
            StopGame();
        }

        private void AddSelection(SocketUser player, RockPaperScissors selection)
        {
            if (players.FirstPlayer == null)
            {
                players.FirstPlayer = player;
                players.FirstPlayerSelection = selection;
            }
            else if (players.SecondPlayer == null)
            {
                players.FirstPlayer = player;
                players.FirstPlayerSelection = selection;
            }
            else if (players.PlayersReady)
            {
                Brawl();
            }
        }

        private void HandleTimer(object sender, ElapsedEventArgs e)
        {
            EmbedBuilder message = new EmbedBuilder()
                .WithTitle(":scissors: :newspaper: :gem: The brawl has timed out")
                .WithColor(Color.Magenta);

            MessageHandler handler = new MessageHandler();
            handler.SendMessage(message);
            gameRunning = false;
            timer.Stop();
            players = new RpsPlayers();
        }

        private EmbedBuilder[] BuildMessages(SocketUser firstPlayer, SocketUser secondPlayer)
        {
            EmbedBuilder firstPlayerMessage = new EmbedBuilder();
            firstPlayerMessage.WithTitle("RPS is on!")
                .WithDescription($"You've started a rock paper scissors match with {secondPlayer}.\nWrite 'rps selection' or 'rps withdraw' to pussy out.")
                .WithColor(Color.Blue);

            EmbedBuilder secondPlayerMessage = new EmbedBuilder();
            secondPlayerMessage.WithTitle("RPS is on!")
                .WithDescription($"{Context.User.Mention} challanged you to rock paper scissors.\nWrite 'rps selection' or 'rps withdraw' to pussy out.")
                .WithColor(Color.Red);

            EmbedBuilder channelMessage = new EmbedBuilder()
                .WithTitle(":scissors: :newspaper: :gem: A brawl is surely brewing.")
                .WithDescription($"{firstPlayer.Mention} challanged {secondPlayer.Mention} to rock paper scissors!")
                .WithColor(Color.Magenta);

            return new EmbedBuilder[] { channelMessage, firstPlayerMessage, secondPlayerMessage };
        }

        [Command("rock"), RequireContext(ContextType.DM)]
        public async Task RpsSelectRock()
        {
            string title;
            string message;

            if (gameRunning)
            {
                AddSelection(Context.User, RockPaperScissors.rock);
                title = "Selected rock";
                message = "Wait for the other player.";
            }
            else
            {
                title = "No game running";
                message = "Start a game by calling !rps @otherguy";
            }

            EmbedBuilder msg = new EmbedBuilder();
            msg.WithTitle(title)
                .WithDescription(message)
                .WithColor(Color.Orange);

            await UserExtensions.SendMessageAsync(Context.User, "", false, msg);
        }

        [Command("paper"), RequireContext(ContextType.DM)]
        public async Task RpsSelectPaper()
        {
            string title;
            string message;

            if (gameRunning)
            {
                AddSelection(Context.User, RockPaperScissors.paper);
                title = "Selected paper";
                message = "Wait for the other player.";
            }
            else
            {
                title = "No game running";
                message = "Start a game by calling !rps @otherguy";
            }

            EmbedBuilder msg = new EmbedBuilder();
            msg.WithTitle(title)
                .WithDescription(message)
                .WithColor(Color.Orange);

            await UserExtensions.SendMessageAsync(Context.User, "", false, msg);
        }

        [Command("scissors"), RequireContext(ContextType.DM)]
        public async Task RpsSelectScissors()
        {
            string title;
            string message;

            if (gameRunning)
            {
                AddSelection(Context.User, RockPaperScissors.scissors);
                title = "Selected scissors";
                message = "Wait for the other player.";
            }
            else
            {
                title = "No game running";
                message = "Start a game by calling !rps @otherguy";
            }

            EmbedBuilder msg = new EmbedBuilder();
            msg.WithTitle(title)
                .WithDescription(message)
                .WithColor(Color.Orange);

            await UserExtensions.SendMessageAsync(Context.User, "", false, msg);
        }

        [Command("withdraw"), RequireContext(ContextType.DM)]
        public async Task RpsWithdraw()
        {
            StopGame();

            EmbedBuilder msg = new EmbedBuilder();
            msg.WithTitle("Cancelled rock paper scissors.")
                .WithDescription($"You pussied out.")
                .WithColor(Color.Orange);

            await UserExtensions.SendMessageAsync(Context.User, "", false, msg);
        }

        [Command ("help")]
        public async Task OutputHelp()
        {
            EmbedBuilder help = new EmbedBuilder();
            help.WithTitle("Help for !rps command")
                .WithDescription($"usage : !rps @usertag to start the game.")
                .WithColor(Color.Orange);

            await ReplyAsync("", false, help.Build());
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

            timer.Interval = 30000;
            timer.Elapsed += new ElapsedEventHandler(HandleTimer);
            timer.Start();

            gameRunning = true;
        }
    }
}