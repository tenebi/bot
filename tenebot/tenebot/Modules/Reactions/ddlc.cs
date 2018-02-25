using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using tenebot.Services;

namespace tenebot.Modules.Reactions
{
    public class Ddlc : ModuleBase<SocketCommandContext>
    {
        [Command("ddlc")]
        public async Task Hmmm([Remainder]SocketGuildUser user = null)
        {
            string imageUrl = @"dokis/";
            string fullUrl = Settings.BaseHostUrl + imageUrl;

            Random rnd = new Random();
            int selected = rnd.Next(0, 10);

            string fullImageUrl = fullUrl + selected.ToString() + ".jpg";

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithImageUrl(fullImageUrl);
            await ReplyAsync("", false, embed.Build());
        }
    }
}
