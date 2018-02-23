using Discord.Commands;
using Discord.WebSocket;
using System;
using tenebot.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tenebot.Services.AdministrationServices;

namespace tenebot.Modules.Fun
{
    [Group("scramble")]
    public class Scramble : ModuleBase<SocketCommandContext>
    {
        Random rnd = new Random();
        string nickname;


        [Command]
        public async Task scramble(SocketGuildUser user)
        {
            char[] charArray = user.Username.ToCharArray();
            char[] randomArray = charArray.OrderBy(x => rnd.Next()).ToArray();

            for(int i = 0; i <= randomArray.Length - 1; i++)
            {
                randomArray[i] = Char.ToLower(randomArray[i]);
            }

            randomArray[0] = Char.ToUpper(randomArray[0]);

            foreach(char a in randomArray)
            {
                nickname += a;
            }
            

            await user.ModifyAsync(c => c.Nickname = nickname);
        }

        [Command("@everyone")]
        public async Task scrambleAll()
        {
            var server = Settings._client.GetGuild(Context.Guild.Id);
            foreach(SocketGuildUser user in server.Users)
            {
                bool isOwner = CheckIsOwner.check(Context.User);
                if (isOwner) 
                {
                    try
                    {
                        char[] charArray = user.Username.ToCharArray();
                        char[] randomArray = charArray.OrderBy(x => rnd.Next()).ToArray();

                        for (int i = 0; i <= randomArray.Length - 1; i++)
                        {
                            randomArray[i] = Char.ToLower(randomArray[i]);
                        }

                        randomArray[0] = Char.ToUpper(randomArray[0]);

                        foreach (char a in randomArray)
                        {
                            nickname += a;
                        }


                        await user.ModifyAsync(c => c.Nickname = nickname);
                        nickname = "";
                    }
                    catch
                    {
                        nickname = "";
                        continue;
                    }

                }
                else
                {
                    await CheckIsOwner.notOwner(Context.Channel);
                }
            }
        }
    }

}
