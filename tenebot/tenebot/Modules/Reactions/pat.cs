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
    public class pat : ModuleBase<SocketCommandContext>
    {
        [Command("pat")]
        public async Task Pat(SocketGuildUser user)
        {
            string imageBaseUrl = @"http://jovanzlatanovic.com/tenebot/pats/";

            Random rnd = new Random();
            int selected = rnd.Next(1, 13);

            EmbedBuilder embed = new EmbedBuilder()
                .WithImageUrl(imageBaseUrl + selected.ToString() + ".gif")
                .WithDescription($"{Context.User.Mention} pats {user.Mention}");
            await ReplyAsync("", false, embed.Build());

            

        }

    }
}
