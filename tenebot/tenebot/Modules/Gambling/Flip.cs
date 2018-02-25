using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace tenebot.Modules.Gambling
{
    public class Flip : ModuleBase<SocketCommandContext>
    {
        Random rand = new Random();

        [Command("flip")]
        public async Task FlipFunction()
        {
            int a = rand.Next(0, 2);
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($":game_die: {Context.User.Username} flips a coin! :game_die:");

            if (a == 0)
            {
                embed.Description = "It landed on **heads**!";
                embed.ImageUrl = "https://i.ebayimg.com/images/g/3jkAAOSwSzdXBkjI/s-l300.jpg";
            }
            else
            {
                embed.Description = "It landed on **tails**!";
                embed.ImageUrl = "https://i.ebayimg.com/images/g/cDwAAOSwAC5ZbdUz/s-l300.jpg";
            }

            await ReplyAsync("", false, embed.Build());
        }
    }
}
