using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tenebot.Services;

namespace tenebot.Modules.Reactions
{
    public class pat : ModuleBase<SocketCommandContext>
    {
        [Command("pat")]
        public async Task Pat(SocketGuildUser user)
        {
            string folder = @"pats/";
            string fullImageUrl = ImageHandler.RandomImageUrl(folder, 10);

            EmbedBuilder embed = new EmbedBuilder()
                .WithImageUrl(fullImageUrl)
                .WithDescription($"{Context.User.Mention} pats {user.Mention}");
            await ReplyAsync("", false, embed.Build());
        }
    }
}
