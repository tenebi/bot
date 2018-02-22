using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace tenebot.Modules.Utility
{
    
    public class Times : ModuleBase<SocketCommandContext>
    {

        

        [Command("daily")]
        public async Task inputs(string time, [Remainder] string message)
        {
            

        }
    }
}
