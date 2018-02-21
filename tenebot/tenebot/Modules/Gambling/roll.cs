using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenebot.Modules.Gambling
{
    public class Roll : ModuleBase<SocketCommandContext>
    {
        Random rand = new Random();
        string numbers = "";
        [Command("roll")]
        public async Task roll()
        {

            for (int i = 1; i <= 10; i++)
            {

                int randint = rand.Next(9);
                numbers += randint.ToString();
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Check 'em")
                .WithColor(Color.Orange)
                .WithDescription($"{Context.User.Mention} rolls **{numbers}**")
                .WithImageUrl("http://i0.kym-cdn.com/entries/icons/original/000/001/714/americanpsycho.jpg");

            if (numbers[9] == numbers[8])
                builder.Title = "D U B S!";
            else if (numbers[9] == numbers[8] && numbers[8] == numbers[7])
                builder.Title = "T R I P S!";
            else if (numbers[9] == numbers[8] && numbers[8] == numbers[7] && numbers[7] == numbers[6])
                builder.Title = "Q U A D S!";
            else if (numbers[9] == numbers[8] && numbers[8] == numbers[7] && numbers[7] == numbers[6] && numbers[6] == numbers[5])
                builder.Title = "Q  U  I  N  T  S!";


            await ReplyAsync("", false, builder.Build());


        }
    }
}
