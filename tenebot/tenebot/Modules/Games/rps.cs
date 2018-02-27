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
        }

        private static System.Timers.Timer timer = new System.Timers.Timer();
        private static ISocketMessageChannel mainChannel = null;
        private static RpsPlayers players = new RpsPlayers();
        private static bool gameRunning = false;

        private bool IsPrivateMessage(SocketMessage msg)
        {
            return (msg.Channel.GetType() == typeof(SocketDMChannel));
        }

        private void StopGame()
        {
            gameRunning = false;
            timer.Stop();
            mainChannel = null;
            players = new RpsPlayers();
        }

        private void Brawl()
        {
            MessageHandler handler = new MessageHandler();
            SendMessage(handler.BuildEmbed("shit works", $"{players.FirstPlayer}: {players.FirstPlayerSelection} vs {players.SecondPlayerSelection} :{players.SecondPlayer}", Color.Magenta));

            //gamelogic here

            StopGame();
        }

        private void AddSelection(SocketUser player, RockPaperScissors selection)
        {
            if (players.FirstPlayer != null && players.SecondPlayer != null)
            {
                if (player.Id == players.FirstPlayer.Id)
                {
                    players.FirstPlayerSelection = selection;
                }
                else if (player.Id == players.SecondPlayer.Id)
                {
                    players.SecondPlayerSelection = selection;
                }

                if (players.FirstPlayerSelection != RockPaperScissors.nothing && players.SecondPlayerSelection != RockPaperScissors.nothing)
                {
                    Brawl();
                }
            }
            else
            {
                Debugging.Log("RPS AddSelection", "One or both players are nulls, there was an error in setting up the game", LogSeverity.Error);
            }
        }

        private void HandleTimer(object sender, ElapsedEventArgs e)
        {
            EmbedBuilder message = new EmbedBuilder()
                .WithTitle(":scissors: :newspaper: :gem: The brawl has timed out")
                .WithColor(Color.Magenta);

            MessageHandler handler = new MessageHandler();
            SendMessage(message);
            gameRunning = false;
            timer.Stop();
            players = new RpsPlayers();
        }

        public async Task SendMessage(EmbedBuilder message)
        {
            await mainChannel.SendMessageAsync("", false, message.Build());
        }

        [Command("rock"), RequireContext(ContextType.DM)]
        public async Task RpsSelectRock()
        {
            AddSelection(Context.User, RockPaperScissors.rock);
            MessageHandler handler = new MessageHandler();
            await UserExtensions.SendMessageAsync(Context.User, "", false, handler.BuildEmbed("Selected rock, wait for the other person.", "", Color.Magenta));
        }

        [Command("paper"), RequireContext(ContextType.DM)]
        public async Task RpsSelectPaper()
        {
            AddSelection(Context.User, RockPaperScissors.paper);
            MessageHandler handler = new MessageHandler();
            await UserExtensions.SendMessageAsync(Context.User, "", false, handler.BuildEmbed("Selected paper, wait for the other person.", "", Color.Magenta));
        }

        [Command("scissors"), RequireContext(ContextType.DM)]
        public async Task RpsSelectScissors()
        {
            AddSelection(Context.User, RockPaperScissors.scissors);
            MessageHandler handler = new MessageHandler();
            await UserExtensions.SendMessageAsync(Context.User, "", false, handler.BuildEmbed("Selected scissors, wait for the other person.", "", Color.Magenta));
        }

        [Command("withdraw"), RequireContext(ContextType.DM)]
        public async Task RpsWithdraw()
        {
            StopGame();
            MessageHandler handler = new MessageHandler();
            await UserExtensions.SendMessageAsync(Context.User, "", false, handler.BuildEmbed("Cancelled rock paper scissors", "You pussied out.", Color.Orange).Build());
            await mainChannel.SendMessageAsync("", false, handler.BuildEmbed("Cancelled rock paper scissors", $"{Context.User.Mention} pussied out.", Color.Magenta).Build());
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
            if (gameRunning)
            {
                MessageHandler handler = new MessageHandler();
                await ReplyAsync("", false, handler.BuildEmbed("Game is already running.", "", Color.Magenta).Build());
            }
            else
            {
                SocketUser firstPlayer = Context.User;

                MessageHandler handler = new MessageHandler();
                EmbedBuilder msg_player1 = handler.BuildEmbed("RPS is on!", $"You've started a rock paper scissors match with {secondPlayer}.\nWrite 'rps selection' or 'rps withdraw' to pussy out.", Color.Magenta);
                EmbedBuilder msg_player2 = handler.BuildEmbed("RPS is on!", $"{Context.User.Mention} challanged you to rock paper scissors.\nWrite 'rps selection' or 'rps withdraw' to pussy out.", Color.Magenta);
                EmbedBuilder msg_channel = handler.BuildEmbed(":gem: :newspaper: :scissors: A brawl is surely brewing.", $"{firstPlayer.Mention} challanged {secondPlayer.Mention} to rock paper scissors!", Color.Magenta);

                await ReplyAsync("", false, msg_channel.Build());
                await UserExtensions.SendMessageAsync(firstPlayer, "", false, msg_player1);
                await UserExtensions.SendMessageAsync(secondPlayer, "", false, msg_player2);

                players.FirstPlayer = firstPlayer;
                players.SecondPlayer = secondPlayer;

                mainChannel = Context.Channel;
            
                timer.Interval = 30000;
                timer.Elapsed += new ElapsedEventHandler(HandleTimer);
                timer.Start();

                gameRunning = true;
            }
        }
    }
}