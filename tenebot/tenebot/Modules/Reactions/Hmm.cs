using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace tenebot.Modules.Reactions
{
    public class Hmm : ModuleBase<SocketCommandContext>
    {
        Random rand = new Random();
        string[] links = { "https://media.giphy.com/media/CaiVJuZGvR8HK/giphy.gif", "https://media1.tenor.com/images/35e1165a21fdaac6f3dc0af49510c471/tenor.gif?itemid=9845045", "https://media.giphy.com/media/CaiVJuZGvR8HK/giphy.gif", "https://media.giphy.com/media/xUPGcz2H1TXdCz4suY/giphy.gif", "https://media.giphy.com/media/TPl5N4Ci49ZQY/giphy.gif", "https://media.giphy.com/media/a5viI92PAF89q/giphy.gif" };

        [Command("hmm")]
        public async Task Hmmm([Remainder]SocketGuildUser user = null)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(":thinking: hmmm :thinking:")
                .WithDescription($"{Context.User.Mention} is suspecting something...")
                .WithImageUrl(links[rand.Next(links.Length)])
                .WithColor(Color.Orange);

            if (user == null)
                await ReplyAsync("", false, builder.Build());
            else
            {
                builder.Description = $"{Context.User.Mention} is suspicious of **{user}**";
                await ReplyAsync("", false, builder.Build());
            }
        }
    }
}
