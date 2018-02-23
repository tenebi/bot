using Discord.Commands;
using tenebot;
using tenebot.Services.AdministrationServices;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using tenebot.Services;

namespace tenebot.Modules.AdministrationCommands
{
    public class UserStats : ModuleBase<SocketCommandContext>
    {
        [Command("getstats")]
        public async Task getStats()
        {
            
            bool isOwner = CheckIsOwner.check(Context.User);
            SocketGuild server = Settings._client.GetGuild(Context.Guild.Id);
            var QueryChannel = server.TextChannels.Where(c => c.Name == "administration");
            

        }
    }
}
