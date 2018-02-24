using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using tenebot.Services;

namespace tenebot.Services.AdministrationServices
{
    public static class CheckIsOwner
    { 
        /// <summary>
        /// Checks if the id is an OwnerId in the configuration.json.
        /// </summary>
        /// <param name="user">SocketGuildUser, use Context.User for this.</param>
        /// <returns>If the user is an owner.</returns>
        public static bool check(SocketUser user)
        {
            foreach (string ownerid in Settings.OwnerIds)
            {
                if (user.Id.ToString() == ownerid)
                    return true;
            }
            
            Debugging.Log($"Administration", $"Administratrative command called by user with insufficient permissions: {user.Username} . If this command call was by you or an administrator, place your client's ID in configuration.json.", Discord.LogSeverity.Warning);
            return false;
        }

        public static async Task notOwner(ISocketMessageChannel channel)
        {
            await channel.SendMessageAsync("", false, Embeds.notOwner.Build());
        }

        public static async Task insufficientPermission(ISocketMessageChannel channel, string message)
        {
            Embeds.notOwner.Description = message;
            await channel.SendMessageAsync("", false, Embeds.notOwner.Build());
        }
    }
}
