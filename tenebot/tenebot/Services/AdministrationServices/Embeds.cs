using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenebot.Services.AdministrationServices
{
    public class Embeds
    {
        public static EmbedBuilder notOwner = new EmbedBuilder();

        public static void InitializeAdminEmbeds()
        {
            notOwner.WithTitle(":no_entry_sign: Insufficient Permissions")
                .WithColor(Color.DarkRed)
                .WithDescription("You need to be an administrator/owner to run this command!");
        }
    }
}
