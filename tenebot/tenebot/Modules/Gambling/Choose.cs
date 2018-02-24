using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace tenebot.Modules.Gambling
{
    public class Choose : ModuleBase<SocketCommandContext>
    {
        [Command("choose")]
        public async Task ChoiceFunction(string args)
        {
            List<string> seperated = args.Split(',').ToList();

            Random rnd = new Random();
            int selection = rnd.Next(seperated.Count);

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($"I have chosen: {seperated[selection]}");

            await ReplyAsync("", false, embed.Build());
        }
    }
}
