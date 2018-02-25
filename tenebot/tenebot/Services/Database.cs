using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenebot.Services
{
    public static class Database
    {
        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TenebotDatabase.mdf;Integrated Security=False";

        public static void TestDatabase()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string command = "INSERT INTO Users(UserId, Username) VALUES ('1234567893', 'test user')";

                using (SqlCommand sqlcommand = new SqlCommand(command, connection))
                {
                    sqlcommand.ExecuteNonQuery();

                    Debugging.Log("Database testing", "shit worked", Discord.LogSeverity.Debug);
                }
            }
        }
    }
}
