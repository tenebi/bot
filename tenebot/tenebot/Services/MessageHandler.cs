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
        public EmbedBuilder BuildEmbed(string title, string description, Color color)
        {
            color = Color.LightGrey;

            EmbedBuilder message = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(color);

            return message;
        }
    }
}
