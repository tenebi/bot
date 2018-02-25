using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace tenebot.Modules.Gambling
{
    public class Roll : ModuleBase<SocketCommandContext>
    {
        Random rand = new Random();
        string numbers = "";
        char[] num = new char[9]; 

        [Command("roll")]
        public async Task RollFunction()
        {
            for (int i = 0; i < 9; i++)
            {
                int randint = rand.Next(9);
                num[i] = Convert.ToChar(randint);
                numbers += randint.ToString();
            }

            Array.Reverse(num);
            char p = num[0];
            int count = 0;

            foreach (char c in num)
            {
                if(c == p)
                    count++;
                else
                    break;

                p = c;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Check 'em")
                .WithColor(Color.Orange)
                .WithDescription($"{Context.User.Mention} rolls **{numbers}**")
                .WithImageUrl("http://i0.kym-cdn.com/entries/icons/original/000/001/714/americanpsycho.jpg");

            switch (count)
            {
                case 1 :
                    builder.Title = "singles :(";
                    builder.ImageUrl = "https://media1.tenor.com/images/785a0f1ad80eb672d1cc792e74d4071b/tenor.gif?itemid=5036238";
                    break;
                case 2 :
                    builder.Title = "D U B S!";
                    builder.ImageUrl = "http://i0.kym-cdn.com/photos/images/newsfeed/000/197/503/1320813769830.gif";
                    break;
                case 3 :
                    builder.Title = "*T R I P S!*";
                    builder.ImageUrl = "http://i0.kym-cdn.com/photos/images/newsfeed/000/527/730/440.gif";
                    break;
                case 4 :
                    builder.Title = "**Q U A D S!**";
                    builder.ImageUrl = "http://i0.kym-cdn.com/photos/images/newsfeed/000/597/416/9e7.gif";
                    break;
                case 5 :
                    builder.Title = "***Q  U  I  N  T  S!***";
                    builder.ImageUrl = "http://i0.kym-cdn.com/photos/images/newsfeed/000/731/269/89a.gif";
                    break;
            }

            await ReplyAsync("", false, builder.Build());
        }
    }
}
