using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace tenebot.Services
{
    public class MessageHandler : ModuleBase<SocketCommandContext>
    {
        public async Task SendMessage(EmbedBuilder message)
        {
            await ReplyAsync("", false, message.Build());
        }
    }
}
