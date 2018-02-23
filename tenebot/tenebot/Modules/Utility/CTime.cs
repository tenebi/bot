using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace tenebot.Modules.Utility
{
    [Group("time")]
    public class WhatTime :  ModuleBase<SocketCommandContext>
    {
        [Command("")]
        public async Task outputTimeHelp()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($"{Context.User.Username}, !time usage:");
            embed.Description = "!time timezone +-n\n\nExamples:\n!time utc\n!time utc +2";
            await ReplyAsync("", false, embed.Build());
        }

        [Command("UTC")]
        public async Task outputTimeUtc(int add)
        {
            DateTime nowtime = DateTime.UtcNow;
            if ((add == 0))
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithTitle($"{Context.User.Username}, the current time in UTC is : (no int or int is 0)");
                embed.Description = nowtime.ToShortTimeString();
                await ReplyAsync("", false, embed.Build());
            }

            else if (add > 0)
            {
                nowtime.AddHours(add);

                EmbedBuilder embed = new EmbedBuilder();
                embed.WithTitle($"{Context.User.Username}, the current time in UTC+"+ add.ToString() +" is : (positive int)");
                embed.Description = nowtime.ToShortTimeString();
                await ReplyAsync("", false, embed.Build());
            }

            else if (add < 0)
            {
                nowtime.AddHours(add);

                EmbedBuilder embed = new EmbedBuilder();
                embed.WithTitle($"{Context.User.Username}, the current time in UTC" + add.ToString() + " is : (negative int)");
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
