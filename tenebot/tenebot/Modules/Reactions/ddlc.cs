using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace tenebot.Modules.Reactions
{
    public class Ddlc : ModuleBase<SocketCommandContext>
    {
        [Command("ddlc")]
        public async Task Hmmm([Remainder]SocketGuildUser user = null)
        {
            string imageBaseUrl = @"http://jovanzlatanovic.com/tenebot/dokis/";

            Random rnd = new Random();
            int selected = rnd.Next(0, 10);

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithImageUrl(imageBaseUrl + selected.ToString() + ".jpg");
            await ReplyAsync("", false, embed.Build());
        }
    }
}
