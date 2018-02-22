using Discord;
using Discord.Commands;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace tenebot.Modules.Reactions
{
    public class weeb : ModuleBase<SocketCommandContext>
    {
        
        //string[] links = { "https://media.giphy.com/media/CaiVJuZGvR8HK/giphy.gif", "https://media1.tenor.com/images/35e1165a21fdaac6f3dc0af49510c471/tenor.gif?itemid=9845045", "https://media.giphy.com/media/CaiVJuZGvR8HK/giphy.gif", "https://media.giphy.com/media/xUPGcz2H1TXdCz4suY/giphy.gif", "https://media.giphy.com/media/TPl5N4Ci49ZQY/giphy.gif", "https://media.giphy.com/media/a5viI92PAF89q/giphy.gif" };
        

        Random rand = new Random();
        int n = Directory.GetFiles(@"C:\Users\Tom\Desktop\shitty bot proj\bot\tenebot\tenebot\weebimages", " *.*", SearchOption.AllDirectories).Length;


        //System.IO.Directory = "";
        [Command("weeb")]
        public async Task weebT([Remainder]string user = null)
        {
            int naem = rand.Next(1, (n+1));
            string faile = naem.ToString() + ".jpg";

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("fucking weebs")
                .WithDescription($"{Context.User.Mention} is being a giant weeaboo")
                .WithImageUrl(faile)
                .WithColor(Color.Orange);

            if (user == null)
            {
                await ReplyAsync("", false, builder.Build());

            }
            else
            {
                builder.Description = $"{Context.User.Mention} is weebing **{user}**";
                await ReplyAsync("", false, builder.Build());
            }
        }
    }
}
