using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace tenebot.Services
{
    class AddToDatabase
    {
        private static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TenebotDatabase.mdf;Integrated Security=True";

        public static Task addToDatabase(SocketGuildUser user)
        {
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

        }
    }
}
