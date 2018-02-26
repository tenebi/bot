using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tenebot.Services;

namespace tenebot.Modules.Reactions
{
    public class pat : ModuleBase<SocketCommandContext>
    {
        string connectionString = Database.ConnectionString;

        [Command("pat")]
        public async Task Pat(SocketGuildUser user)
        {
            string folder = @"pats/";
            string fullImageUrl = ImageHandler.RandomImageUrl(folder, 10);


            EmbedBuilder embed = new EmbedBuilder()
                .WithImageUrl(fullImageUrl)
                .WithDescription($"{Context.User.Mention} pats {user.Mention}");
            await ReplyAsync("", false, embed.Build());

            UpdatePats(user);

        }

        public void UpdatePats(SocketGuildUser user)
        {
            string queryPats = $"SELECT Pats FROM Users WHERE UserId = {user.Id}";
            int pats = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryPats, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pats = reader.GetInt16(0);
                        }
                    }
                }
                connection.Close();
            }
            string update = $"UPDATE Users SET Pats = {pats} WHERE UserId = {user.Id}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(update, connection))
                {
                    command.ExecuteNonQuery();
                }


            }
        }
    }
}