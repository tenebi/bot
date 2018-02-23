using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace tenebot.Modules.Utility
{
    [Group("time")]
    public class WhatTime :  ModuleBase<SocketCommandContext>
    {
        [Command("UTC")]
        public async Task outputTimeUtc(int add)
        {
            DateTime nowtime = DateTime.UtcNow;
            if ((add == null) || (add == 0))
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithTitle($"{Context.User.Username}, the current time in UTC is : ");
                embed.Description = nowtime.ToShortTimeString();
                await ReplyAsync("", false, embed.Build());
            }

            else if (add > 0)
            {
                nowtime.AddHours(add);

                EmbedBuilder embed = new EmbedBuilder();
                embed.WithTitle($"{Context.User.Username}, the current time in UTC+"+ add.ToString() +" is : ");
                embed.Description = nowtime.ToShortTimeString();
                await ReplyAsync("", false, embed.Build());
            }

            else if (add < 0)
            {
                nowtime.AddHours(add);

                EmbedBuilder embed = new EmbedBuilder();
                embed.WithTitle($"{Context.User.Username}, the current time in UTC" + add.ToString() + " is : ");
                embed.Description = nowtime.ToShortTimeString();
                await ReplyAsync("", false, embed.Build());
            }
        }

        [Command("local")]
        public async Task outputTimeGmt()
        {
            DateTime nowtime = DateTime.Now;

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($"{Context.User.Username}, the current local time is is : ");
            embed.Description = nowtime.ToShortTimeString();
            await ReplyAsync("", false, embed.Build());
        }
    }
}
