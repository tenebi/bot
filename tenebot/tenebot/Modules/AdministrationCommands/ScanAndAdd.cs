using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using tenebot.Services.AdministrationServices;
using System.Threading.Tasks;
using tenebot.Services;
/*
namespace tenebot.Modules.AdministrationCommands
{
    public class ScanAndAdd : ModuleBase<SocketCommandContext>
    {
        List<string> databaseUsers = new List<string>();
        private static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TenebotDatabase.mdf;Integrated Security=False";
        [Command("scan")]
        public async Task Scan()
        {
            bool isOwner = Services.AdministrationServices.CheckIsOwner.check(Context.User);
            var Users = Context.Guild.Users;
            List<String> databaseUsers = new List<String>();
            List<String> serverUsers = new List<String>();
            List<String> unlistedUsers = new List<String>();
            if (isOwner)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT UserId FROM Users";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                databaseUsers.Add(reader.GetString(0));
                            }
                        }
                    }
                    connection.Close();
                }

                Debugging.Log("AddToDatabase", $"Length of database {databaseUsers.Count}");


                foreach (SocketGuildUser user in Users)
                {
                    serverUsers.Add(user.Id.ToString());
                }

                unlistedUsers = serverUsers.Except(databaseUsers).ToList();

                foreach (string userId in unlistedUsers)
                {
                    string Username = Context.Guild.GetUser(ulong.Parse(userId)).ToString();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        string command = $"INSERT INTO Users(UserId, Username) VALUES ('{userId}', '{Username}')";

                        using (SqlCommand sqlcommand = new SqlCommand(command, connection))
                        {
                            sqlcommand.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    Debugging.Log("AddToDatabase", $"Added user {Username} to database with Id {userId}", Discord.LogSeverity.Info);

                }
            }
            else
            {
                await ReplyAsync("", false, Embeds.notOwner.Build());
            }


        }

    }
<<<<<<< HEAD
}

=======
}*/
>>>>>>> develop
