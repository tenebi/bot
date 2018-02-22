using Discord;
using Discord.Commands;
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
        string[] links = {  "https://www.jovanzlatanovic.com:2083/cpsess3686105827/viewer/home%2fjovanzl%2fpublic_html%2ftenebi%2ftoms_bullshit/1.jpg",
                            "https://www.jovanzlatanovic.com:2083/cpsess3686105827/frontend/paper_lantern/filemanager/showfile.html?file=2.jpg&fileop=&dir=%2Fhome%2Fjovanzl%2Fpublic_html%2Ftenebi%2Ftoms_bullshit&dirop=&charset=&file_charset=&baseurl=&basedir=",
                            "https://www.jovanzlatanovic.com:2083/cpsess3686105827/frontend/paper_lantern/filemanager/showfile.html?file=3.jpg&fileop=&dir=%2Fhome%2Fjovanzl%2Fpublic_html%2Ftenebi%2Ftoms_bullshit&dirop=&charset=&file_charset=&baseurl=&basedir=" };
        [Command("weeb")]
        public async Task weebT([Remainder]string user = null)
        {

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("fucking weebs")
                .WithDescription($"{Context.User.Mention} is being a dirty weeaboo")
                .WithImageUrl(links[rand.Next(links.Length)])
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
