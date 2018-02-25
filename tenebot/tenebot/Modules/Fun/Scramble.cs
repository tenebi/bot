using Discord.Commands;
using Discord.WebSocket;
using System;
using tenebot.Services;
using System.Linq;
using System.Threading.Tasks;
using tenebot.Services.AdministrationServices;

namespace tenebot.Modules.Fun
{
    [Group("scramble")]
    public class Scramble : ModuleBase<SocketCommandContext>
    {
        Random rnd = new Random();

        private string ScrambleName(string name)
        {
            char[] charArray = name.ToCharArray();
            char[] randomArray = charArray.OrderBy(x => rnd.Next()).ToArray();

            string nickname = "";

            for (int i = 0; i <= randomArray.Length - 1; i++)
                randomArray[i] = Char.ToLower(randomArray[i]);

            randomArray[0] = Char.ToUpper(randomArray[0]);

            foreach (char a in randomArray)
                nickname += a;

            return nickname;
        }

        [Command]
        public async Task ScrambleFunction(SocketGuildUser user) => await user.ModifyAsync(c => c.Nickname = ScrambleName(user.Username));

        [Command("@everyone")]
        public async Task ScrambleAllFunction()
        {
            var server = Settings._client.GetGuild(Context.Guild.Id);

            foreach(SocketGuildUser user in server.Users)
            {
                if (CheckIsOwner.Check(Context.User)) 
                {
                    try
                    {
                        await user.ModifyAsync(c => c.Nickname = ScrambleName(user.Username));
                    }
                    catch
                    {
                        continue;
                    }
                }
                else
                    await CheckIsOwner.NotOwner(Context.Channel);
            }
        }
    }

}
