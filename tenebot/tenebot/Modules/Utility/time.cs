using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace tenebot.Modules.Utility
{
    [Group("time")]
    public class WhatTime :  ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task OutputTimeHelp()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($"{Context.User.Username}, !time usage:");
            embed.Description = "!time timezone +-n\n\nExamples:\n!time utc\n!time utc +2";
            await ReplyAsync("", false, embed.Build());
        }

        [Command("UTC")]
        public async Task OutputTimeUtc(double add = 0)
        {
            DateTime nowtime = DateTime.UtcNow;
            EmbedBuilder embed = new EmbedBuilder();

            if ((add == 0))
                embed.WithTitle($"{Context.User.Username}, the current time in UTC is :");
            else if (add > 0)
            {
                nowtime = nowtime.AddHours(add);
                embed.WithTitle($"{Context.User.Username}, the current time in UTC+"+ add.ToString() +" is :");
            }
            else if (add < 0)
            {
                nowtime = nowtime.AddHours(add);
                embed.WithTitle($"{Context.User.Username}, the current time in UTC" + add.ToString() + " is :");
            }

            embed.Description = nowtime.ToShortTimeString();
            await ReplyAsync("", false, embed.Build());
        }

        [Command("GMT")]
        public async Task OutputTimeGmt(double add = 0)
        {
            DateTime nowtime = DateTime.UtcNow;
            EmbedBuilder embed = new EmbedBuilder();

            if ((add == 0))
                embed.WithTitle($"{Context.User.Username}, the current time in GMT is :");
            else if (add > 0)
            {
                nowtime = nowtime.AddHours(add);
                embed.WithTitle($"{Context.User.Username}, the current time in GMT+" + add.ToString() + " is :");
            }
            else if (add < 0)
            {
                nowtime = nowtime.AddHours(add);
                embed.WithTitle($"{Context.User.Username}, the current time in GMT" + add.ToString() + " is :");
            }

            embed.Description = nowtime.ToShortTimeString();
            await ReplyAsync("", false, embed.Build());
        }

        [Command("local")]
        public async Task OutputTimeGmt()
        {
            DateTime localtime = DateTime.Now;

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($"{Context.User.Username}, the current local time is : ");
            embed.Description = localtime.ToShortTimeString();
            await ReplyAsync("", false, embed.Build());
        }
    }
}
