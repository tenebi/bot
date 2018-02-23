using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace tenebot.Modules.Utility
{
    public class WhatTime :  ModuleBase<SocketCommandContext>
    {
        [Command("time")]
        public async Task outputTime()
        {
            DateTime nowtime = DateTime.UtcNow;

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($"{Context.User.Username}, the current time in UTC is : " );
            embed.Description = nowtime.ToShortTimeString();
            await ReplyAsync("", false, embed.Build());
        }
    }
}
