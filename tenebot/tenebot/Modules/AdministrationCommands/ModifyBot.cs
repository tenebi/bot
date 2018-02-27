﻿using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tenebot.Services.AdministrationServices;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;

namespace tenebot.Modules.AdministrationCommands
{
    [Group("modify")]
    public class ModifyBot : ModuleBase<SocketCommandContext>
    {


        [Command("username")]
        public async Task modifyUsername([Remainder] string username)
        {
            bool isOwner = CheckIsOwner.Check(Context.User);
            if (isOwner)
            {
                await Context.Client.CurrentUser.ModifyAsync(c => c.Username = username);
            }
            else
            {
                await ReplyAsync("", false, Embeds.notOwner.Build());
            }
        }
    }
}
