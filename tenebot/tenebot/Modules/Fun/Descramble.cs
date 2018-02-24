using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tenebot.Services;

namespace tenebot.Modules.Fun
{
    [Group("descramble")]
    public class Descramble : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task descramble(SocketGuildUser user)
        {
            await user.ModifyAsync(c => c.Nickname = user.Username);
        }
        [Command("@everyone")]
        public async Task descrambleAll()
        {
            var server = Settings._client.GetGuild(Context.Guild.Id);
            foreach(SocketGuildUser user in server.Users)
            {
                try
                {
                    await user.ModifyAsync(c => c.Nickname = user.Username);
                }
                catch
                {
                    continue;
                }
            }
        }

    }
}
