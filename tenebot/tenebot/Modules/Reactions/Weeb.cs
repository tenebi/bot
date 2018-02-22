using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenebot.Modules.Reactions
{
    public class weeb : ModuleBase<SocketCommandContext>
    {
        Random rand = new Random();
        string input;
        string[] links = {  "https://www.jovanzlatanovic.com:2083/cpsess3686105827/viewer/home%2fjovanzl%2fpublic_html%2ftenebi%2ftoms_bullshit/1.jpg",
                            "https://www.jovanzlatanovic.com:2083/cpsess3686105827/viewer/home%2fjovanzl%2fpublic_html%2ftenebi%2ftoms_bullshit/2.jpg",
                            "https://www.jovanzlatanovic.com:2083/cpsess3686105827/viewer/home%2fjovanzl%2fpublic_html%2ftenebi%2ftoms_bullshit/3.jpg" };
        [Command("weeb")]
<<<<<<< Updated upstream
        public async Task weebT(string user)
        {
=======
        public async Task weebT(string user = null)
        {
            var guild = Context.Guild;
>>>>>>> Stashed changes
            EmbedBuilder builder = new EmbedBuilder();
            if (user.Contains("@") == false)
            {
                builder.WithTitle("fucking weebs")
                    .WithDescription($"{Context.User.Mention} is being a dirty weeaboo")
                    .WithImageUrl(links[rand.Next(links.Length)])
                    .WithColor(Color.Orange);
            }

<<<<<<< Updated upstream
            else if (user == null)
=======
            if (user[0] != '@')
>>>>>>> Stashed changes
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
