using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tenebot.Services;
/*
namespace tenebot.Modules.AdministrationCommands
{
    public class ScanAndAdd : ModuleBase<SocketCommandContext>
    {
        List<string> databaseUsers = new List<string>();
        private static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TenebotDatabase.mdf;Integrated Security=True";
        [Command("scan")]
        public async Task Scan()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string selectAll = $"SELECT UserId FROM Users";

                using (SqlCommand sqlcommand = new SqlCommand(selectAll, connection))
                {
                    sqlcommand.ExecuteNonQuery();
                    using (SqlDataReader reader = selectAll.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            databaseUsers.Add(reader.GetString(0));
                        }
                    }

                }
            }
            var Users = Context.Guild.Users;
            


        }

    }
}

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string command = $"INSERT INTO Users(UserId, Username) VALUES ('{user.Id}', '{user.Username}')";

                using (SqlCommand sqlcommand = new SqlCommand(command, connection))
                {
                    sqlcommand.ExecuteNonQuery();

                    Debugging.Log("DATABASE", $"User {user.Username} with ID: {user.Id} was added to the database.", Discord.LogSeverity.Info);
                }
            }

            return Task.CompletedTask;


    List<String> columnData = new List<String>();

using(SqlConnection connection = new SqlConnection("conn_string"))
{
    connection.Open();
    string query = "SELECT Column1 FROM Table1";
    using(SqlCommand command = new SqlCommand(query, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                columnData.Add(reader.GetString(0));
            }         
        }
    }
}*/