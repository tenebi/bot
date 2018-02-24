using Discord.Commands;
using tenebot;
using tenebot.Services.AdministrationServices;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tenebot.Services;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace tenebot.Modules.AdministrationCommands
{
    public class UserStats : ModuleBase<SocketCommandContext>
    {
        [Command("getstats")]
        public async Task getStats()
        {
            bool isOwner = CheckIsOwner.check(Context.User);
            SocketGuild server = Settings._client.GetGuild(Context.Guild.Id);

            if (!isOwner)
                await ReplyAsync("", false, Embeds.notOwner.Build());
            else
            {
                try
                {
                    var AdminChannel = server.TextChannels.Where(c => c.Name == Settings.AdminChannel).FirstOrDefault();
                    await AdminChannel.SendMessageAsync("hello");
                }
                catch
                {
                    string msg = $"No channel called #{Settings.AdminChannel}. Please create a text channel labeled #{Settings.AdminChannel} to use administrative commands.";

                    Debugging.Log("Administration", msg, LogSeverity.Error);

                    EmbedBuilder embed = new EmbedBuilder() { Title = "Error", Description = msg };
                    await ReplyAsync("", false, embed.Build());
                }
            }
        }
    }
}
