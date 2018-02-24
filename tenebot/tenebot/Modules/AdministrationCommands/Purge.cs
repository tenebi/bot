using Discord.Commands;
using Discord.WebSocket;
using tenebot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tenebot.Services.AdministrationServices;
using Discord;

namespace tenebot.Modules.AdministrationCommands
{
    public class Purge : ModuleBase<SocketCommandContext>
    {



        public async Task purgeMessages(SocketGuildUser user, int amount)
        {
            var channel = Context.Channel;
            await channel.DeleteMessagesAsync(channel.CachedMessages.OrderBy(x => x.Timestamp).Where(c => c.Author == user).Take(amount));
            
        }



        [Command("purge")]
        public async Task purge(SocketGuildUser user, int amount)
        {
            var channel = Context.Channel;
            var userAsSocket = Settings._client.GetGuild(Context.Guild.Id).GetUser(Context.User.Id);
            bool userHasPermission = userAsSocket.GuildPermissions.ManageMessages;

            EmbedBuilder embed2 = new EmbedBuilder()
                .WithTitle($":speak_no_evil: Successfully purged messages.")
                .WithDescription($"Successfully purged {amount} of {user.Mention}'s messages.")
                .WithColor(Color.DarkRed);
            if(userHasPermission)
            {
                try
                {
                    await purgeMessages(user, amount);
                    try
                    {
                        var AdminChannel = Context.Guild.TextChannels.Where(c => c.Name == Settings.AdminChannel).FirstOrDefault();
                        await AdminChannel.SendMessageAsync("", false, embed2.Build());
                    }
                    catch
                    {
                        string msg = $"No channel called #{Settings.AdminChannel}. Please create a text channel labeled #{Settings.AdminChannel} to use administrative commands.";

                        Debugging.Log("Administration", msg, LogSeverity.Error);

                        EmbedBuilder embed = new EmbedBuilder() { Title = "Error", Description = msg };
                        await ReplyAsync("", false, embed.Build());
                    }
                }
                catch(Exception e)
                {
                    Debugging.Log("ADMIN | PURGE", e.Message, Discord.LogSeverity.Warning);
                }
            }
            else
            {
                await CheckIsOwner.insufficientPermission(Context.Channel, "You need to be able to Manage Messages to call this!");
            }
        }
    }
}
