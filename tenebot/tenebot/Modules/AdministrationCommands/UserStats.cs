using Discord.Commands;
using tenebot;
using tenebot.Services.AdministrationServices;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tenebot.Services;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace tenebot.Modules.AdministrationCommands
{
    public class UserStats : ModuleBase<SocketCommandContext>
    {
        [Command("getstats")]
        public async Task getStats()
        {
            
            bool isOwner = CheckIsOwner.check(Context.User);
            SocketGuild server = Settings._client.GetGuild(Context.Guild.Id);

            
            

            if(!isOwner)
            {
                await ReplyAsync("", false, Embeds.notOwner.Build());
            }


            else
            {
                try
                {
                    var AdminChannel = server.TextChannels.Where(c => c.Name == "administration").FirstOrDefault();
                    await AdminChannel.SendMessageAsync("hello");
                }
                catch
                {
                    Debugging.Log("ADMINISTRATION", "No channel called #administration. Please create a text channel labeled #administration to use administrative commands.", LogSeverity.Critical);
                }
            }
            
            

        }
    }
}
