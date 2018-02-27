using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using tenebot.Services.AdministrationServices;
using System.Threading.Tasks;
using tenebot.Services;
using Discord;

namespace tenebot.Modules.AdministrationCommands
{
    public class ScanAndAdd : ModuleBase<SocketCommandContext>
    {
        List<string> databaseUsers = new List<string>();
        private static string ConnectionString = SqlHandler.GetConnectionString(); //Database.ConnectionString;

        [Command("scan")]
        public async Task Scan()
        {
            bool isOwner = CheckIsOwner.Check(Context.User);
            var Users = Context.Guild.Users;

            EmbedBuilder scanned = new EmbedBuilder()
                .WithColor(Color.LightOrange)
                .WithTitle("Scanned server and added to database.");
                
            List<String> databaseUsers = new List<String>();
            List<String> serverUsers = new List<String>();
            List<String> unlistedUsers = new List<String>();

            if (isOwner)
            {
                databaseUsers = SqlHandler.Select("User", "UserId", "");
                Debugging.Log("AddToDatabase", $"Length of database {databaseUsers.Count}");

                foreach (SocketGuildUser user in Users)
                {
                    serverUsers.Add(user.Id.ToString());
                }

                unlistedUsers = serverUsers.Except(databaseUsers).ToList();

                foreach (string userId in unlistedUsers)
                {
                    string Username = Context.Guild.GetUser(ulong.Parse(userId)).ToString();

                    SqlHandler.Insert("User(UserId, Username)", $"('{userId}', '{Username}')");

                    Debugging.Log("AddToDatabase", $"Added user {Username} to database with Id {userId}", Discord.LogSeverity.Info);
                    scanned.AddField($"Added user {Username} to database with ID {userId}", "\n");
                }

                await ReplyAsync("", false, scanned.Build());
            }
            else
                await ReplyAsync("", false, Embeds.notOwner.Build());
        }
    }
}