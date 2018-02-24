using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace tenebot.Modules.Utility
{
    [Group("timer")]
    public class Timer : ModuleBase<SocketCommandContext>
    {
        System.Timers.Timer aTimer = new System.Timers.Timer();
        string timerMessage;

        public async Task TimerDone(string message)
        {
            EmbedBuilder doneBuilder = new EmbedBuilder();
            doneBuilder.WithTitle(":stopwatch: Brring! :stopwatch:")
                .WithDescription($"{Context.User.Mention}, {message}!")
                .WithColor(Color.Orange);

            await ReplyAsync(Context.User.Mention);
            await ReplyAsync("", false, doneBuilder.Build());
        }

        void HandleTimer(Object source, ElapsedEventArgs e)
        {
            TimerDone(timerMessage);
            aTimer.Stop();
        }

        [Command("secs")]
        public async Task Secs(int time, [Remainder] string message)
        {
            EmbedBuilder setBuilder = new EmbedBuilder();
            setBuilder.WithTitle($":stopwatch: Timer set! :stopwatch: ")
                .WithDescription($"You will be reminded in **{time}** seconds with the message **{message}**")
                .WithColor(Color.Orange);

            int timesecs = time * 1000;

            await ReplyAsync("", false, setBuilder.Build());
            aTimer.Elapsed += new ElapsedEventHandler(HandleTimer);
            aTimer.Interval = timesecs;
            aTimer.Start();
            timerMessage = message;
        }

        [Command("mins")]
        public async Task Mins(int time, [Remainder] string message)
        {
            EmbedBuilder setBuilder = new EmbedBuilder();
            setBuilder.WithTitle($":stopwatch: Timer set! :stopwatch: ")
                .WithDescription($"You will be reminded in **{time}** minutes with the message **{message}**")
                .WithColor(Color.Orange);

            int timesecs = time * 60000;

            await ReplyAsync("", false, setBuilder.Build());
            aTimer.Elapsed += new ElapsedEventHandler(HandleTimer);
            aTimer.Interval = timesecs;
            aTimer.Start();
            timerMessage = message;
        }

        [Command("hrs")]
        public async Task Hrs(int time, [Remainder] string message)
        {
            EmbedBuilder setBuilder = new EmbedBuilder();
            setBuilder.WithTitle($":stopwatch: Timer set! :stopwatch: ")
                .WithDescription($"You will be reminded in **{time}** hours with the message **{message}**")
                .WithColor(Color.Orange);

            int timesecs = time * 3600000;

            await ReplyAsync("", false, setBuilder.Build());
            aTimer.Elapsed += new ElapsedEventHandler(HandleTimer);
            aTimer.Interval = timesecs;
            aTimer.Start();
            timerMessage = message;
        }
    }
}
