using Discord.Commands;
using tenebot.Services.AdministrationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenebot.Modules.AdministrationCommands
{
    public class UserStats : ModuleBase<SocketCommandContext>
    {
        [Command("getstats")]
        public async Task getStats()
        {
            bool isOwner = CheckIsOwner.check(Context.User);
            if (!isOwner)
            {
                await ReplyAsync("negative captain!");
            }
            else
            {
                await ReplyAsync("positive captain!");
            }

        }
    }
}
