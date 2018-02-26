using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using tenebot.Services;

namespace tenebot
{
    class SqlHandler
    {
        /// <summary>
        /// Database connection string.
        /// </summary>
        public static string ConnectionString;

        private static MySqlConnection DatabaseConnection = new MySqlConnection(ConnectionString);
        private static List<string> FieldNames = new List<string>();
        private static List<string> FieldTypes = new List<string>();

        private static int numberOfFields = 0;

        /// <summary>
        /// Tests the database.
        /// </summary>
        /// <returns>Returns true if it test is successful.</returns>
        public bool TestDatabase()
        {
            try
            {
                MySqlCommand check = new MySqlCommand("SELECT * FROM dbname.users", DatabaseConnection);
                MySqlDataReader reader;

                DatabaseConnection.Open();
                reader = check.ExecuteReader();

                while (reader.Read())
                {
                    Debugging.Log("Database Test", reader.GetInt32("id") + " " + reader.GetString("name"));
                }

                DatabaseConnection.Close();

                return true;
            }
            catch (Exception e)
            {
                Debugging.Log("TestDatabase", $"Error testing database: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// Sets a new connection string and returns it.
        /// </summary>
        /// <param name="url">The database address.</param>
        /// <param name="port">The database port.</param>
        /// <param name="username">Login username.</param>
        /// <param name="password">Login password.</param>
        /// <returns>New connection string.</returns>
        public string SetConnectionString(string url, string port, string username, string password)
        {
            ConnectionString = $"datasource={url};port={port};username={username};password={password};";
            Debugging.Log("SetConnectionString", $"New connection string set and returned: {ConnectionString}");
            return ConnectionString;
        }

        public List<string> Select(string tableName, string select, string where, string databaseName = null)
        {
            databaseName = Settings.DatabaseName;

            MySqlDataReader reader;
            MySqlDataReader reader1;
            
            List<string> memes = new List<string>();

            string tarpmeme;
            string querystring;
            string name;
            string tarp = "";
            string type;

            List<string> returno = new List<string>();

            DatabaseConnection.Open();
            MySqlCommand clmnname = new MySqlCommand("SELECT column_name from information_schema.columns where table_schema = '" + databaseName.ToString() + "' and table_name = '" + tableName.ToString() + "';", DatabaseConnection);
            MySqlCommand clmn = new MySqlCommand("SHOW FIELDS FROM "+ databaseName +"."+ tableName +";", DatabaseConnection);

            reader = clmn.ExecuteReader();

            while (reader.Read())
            {
                name = reader.GetString("Field");
                FieldNames.Add(name);
                type = reader.GetString("Type");
                FieldTypes.Add(type);
                numberOfFields++;
            }
            reader.Close();

            if (where == "")
            {
                MySqlCommand cmd = new MySqlCommand("SELECT " + select.ToString() + " FROM " + databaseName.ToString() + "." + tableName.ToString() + ";", DatabaseConnection);
                reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    for (int f = 0; f < numberOfFields; f++)
                    {
                        querystring ="'" + FieldNames[f] + "'";
                        tarpmeme = reader1.GetString(FieldNames[f].ToString());
                        tarp += tarpmeme + ",";
                    }
                    returno.Add(tarp);
                    tarp = "";
                }
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("SELECT " + select.ToString() + " FROM " + databaseName.ToString() + "." + tableName.ToString() + " WHERE " + where.ToString() + ";", DatabaseConnection);
                
                reader1 = cmd.ExecuteReader();
                while (reader1.Read())
                {
                    for (int f = 0; f < numberOfFields; f++)
                    {
                        querystring = "'" + FieldNames[f] + "'";
                        tarpmeme = reader1.GetString(FieldNames[f].ToString());
                        tarp += tarpmeme + ",";
                    }
                    returno.Add(tarp);
                    tarp = "";
                }
            }
            DatabaseConnection.Close();
            return returno;
        }

        /// <summary>
        /// Deletes values from the database.
        /// </summary>
        /// <param name="tableName">Table from which to delete.</param>
        /// <param name="condition">Conditions for deletion.</param>
        /// <param name="databaseName">Optional name for the database (disregard the null, it's set).</param>
        public void Delete(string tableName, string condition, string databaseName = null)
        {
            databaseName = Settings.DatabaseName;

            MySqlDataReader reader;
            DatabaseConnection.Open();
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM {databaseName}.{tableName} WHERE {condition};", DatabaseConnection);

            try
            {
                reader = cmd.ExecuteReader();
                Debugging.Log("SQL Insert", $"Deleted records from database.");
            }
            catch (Exception e)
            {
                Debugging.Log("SQL Delete", $"Error deleting records: {e.Message}", Discord.LogSeverity.Error);
            }

            DatabaseConnection.Close();
        }

        /// <summary>
        /// Insert a row into the database.
        /// </summary>
        /// <param name="tableName">Table from which to delete.</param>
        /// <param name="values">Values to be insterted.</param>
        /// <param name="databaseName">Optional name for the database (disregard the null, it's set).</param>
        public void Insert(string tablename, string values, string databaseName = null)
        {
            databaseName = Settings.DatabaseName;

            MySqlDataReader reader;
            DatabaseConnection.Open();
            MySqlCommand cmd = new MySqlCommand("Insert Into " + databaseName + "." + tablename + " Values (" + values.ToString() + ");", DatabaseConnection);

            try
            {
                reader = cmd.ExecuteReader();
                Debugging.Log("SQL Insert", $"Inserted records into database.");
            }
            catch (Exception e)
            {
                Debugging.Log("SQL Insert", $"Error inserting records: {e.Message}", Discord.LogSeverity.Error);
            }

            DatabaseConnection.Close();
        }

        /// <summary>
        /// Updates a row in the database.
        /// </summary>
        /// <param name="tableName">Table from which to delete.</param>
        /// <param name="values">Values to be insterted.</param>
        /// <param name="where">Selecting which rows to be affected.</param>
        /// <param name="databaseName">Optional name for the database (disregard the null, it's set).</param>
        public void Update(string tableName, string values, string where, string databaseName = null)
        {
            databaseName = Settings.DatabaseName;

            MySqlDataReader reader;
            DatabaseConnection.Open();
            string set = "";
            int f = 1;

            string[] zodmas = new string[numberOfFields-1];
            char[] skirt = new char[2] {',', ' ' };

            zodmas = values.Split(skirt);

            for(int i = 0; i < numberOfFields-1; i++)
            {
                set += FieldNames[f].ToString() + "=";
                if (i != numberOfFields-2)
                {
                    set += zodmas[i] + ",";
                }
                else
                {
                    set += zodmas[i];
                }
                f++;
            }

            MySqlCommand cmd = new MySqlCommand($"UPDATE {databaseName}.{tableName} SET {set} WHERE {where};", DatabaseConnection);

            try
            {
                reader = cmd.ExecuteReader();
                Debugging.Log("SQL Update", $"Updated records in database.");
            }
            catch (Exception e)
            {
                Debugging.Log("SQL Update", $"Error updating records: {e.Message}", Discord.LogSeverity.Error);
            }

            DatabaseConnection.Close();
        }
    }
}
 

