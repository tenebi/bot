﻿using Discord;
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
        char[] num = new char[9]; 
        [Command("roll")]
        public async Task roll()
        {

            for (int i = 0; i < 9; i++)
            {

                int randint = rand.Next(9);
                num[i] = Convert.ToChar(randint);
                //numbers += randint.ToString();
            }

            Array.Reverse(num);
            char p = num[0];
            int count = 0;

            foreach (char c in num)
            {
                if(c == p)
                {
                    count++;
                }

                else
                {
                    break;
                }
                p = c;
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Check 'em")
                .WithColor(Color.Orange)
                .WithDescription($"{Context.User.Mention} rolls **{numbers}**")
                .WithImageUrl("http://i0.kym-cdn.com/entries/icons/original/000/001/714/americanpsycho.jpg");

            switch (count)
            {
                case 0 :
                    builder.Title = "singles :(";
                    break;
                case 1 :
                    builder.Title = "D U B S!";
                    break;
                case 2 :
                    builder.Title = "*T R I P S!*";
                    break;
                case 3 :
                    builder.Title = "**Q U A D S!**";
                    break;
                case 4 :
                    builder.Title = "***Q  U  I  N  T  S!***";
                    break;
            }

            await ReplyAsync("", false, builder.Build());

            /* else if (count == 1)
                 builder.Title = "D U B S!";
             else if (count == 2)
                 builder.Title = "T R I P S!";
             else if (count == 3)
                 builder.Title = "Q U A D S!";
             else if (count == 4)
                 builder.Title = "Q  U  I  N  T  S!";*/





        }
    }
}