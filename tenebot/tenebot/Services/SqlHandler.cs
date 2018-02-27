using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using tenebot.Services;

namespace tenebot
{
    public static class SqlHandler
    {
        //private static string connectionString = "datasource=localhost;port=3306;username=root;password=password;";
        private static string connectionString;
        private static MySqlConnection DatabaseConnection = new MySqlConnection(connectionString);

        /// <summary>
        /// Attempts to connect to a MySql server based on connection settings either preloaded or passed directly via SetConnectionSTring().
        /// Attempts to retrieve all table names from a specified database (currently database is loaded from a json file).
        /// </summary>
        /// <returns>Returns true if test is successful.</returns>
        public static bool TestDatabase()
        {
            string dbname = Settings.DatabaseName;

            try
            {
                DatabaseConnection.Open();
                MySqlCommand test = new MySqlCommand($"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA='dbname';", DatabaseConnection);
                MySqlDataReader readerlocal1;

                readerlocal1 = test.ExecuteReader();


                while (readerlocal1.Read())
                {
                    Debugging.Log("Database Test", reader.GetString("TABLE_NAME"));
                }
                DatabaseConnection.Close();


                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Debugging.Log("TestDatabase", $"Error testing database: {e.Message}", Discord.LogSeverity.Critical);
                Debugging.Log("TestDatabase", $"Some features which use a mysql database won't be available", Discord.LogSeverity.Warning);
                return false;
            }
            
        }


        /// <summary>
        /// Attempts fech all field names in a given database and table.
        /// Used for internal functions, may be used for getting field names.
        /// </summary>
        /// <param name="databaseName">Selected database.</param>
        /// <param name="tableName">Selected table.</param>
        /// <returns>Returns a string list containing all field names.</returns>
        public static List<string> GetDataBaseFields(string databaseName, string tableName)
        {

            DatabaseConnection.Close();
            DatabaseConnection.Open();
            MySqlCommand clmn = new MySqlCommand("SHOW FIELDS FROM " + databaseName + "." + tableName + ";", DatabaseConnection);
            MySqlDataReader readerlocal2;
            string name;
            List<string> FieldNames = new List<string>();

            readerlocal2 = clmn.ExecuteReader();

            while (readerlocal2.Read())
            {
                name = readerlocal2.GetString("Field");
                FieldNames.Add(name);
            }
            readerlocal2.Close();
            DatabaseConnection.Close();
            return FieldNames;
        }


        /// <summary>
        /// Attempts fech total count of fields withing a given database and table.
        /// Used for internal functions, may be used for getting field names.
        /// </summary>
        /// <param name="databaseName">Selected database.</param>
        /// <param name="tableName">Selected table.</param>
        /// <returns>Returns an int containing total count of fields.</returns>
        public static int GetDataBaseFieldCount(string databaseName, string tableName)
        {


            DatabaseConnection.Open();
            MySqlCommand clmn = new MySqlCommand("SHOW FIELDS FROM " + databaseName + "." + tableName + ";", DatabaseConnection);
            MySqlDataReader reader;
            string name;
            int count = 0;
            List<string> FieldNames = new List<string>();

            reader = clmn.ExecuteReader();

            while (reader.Read())
            {
                name = reader.GetString("Field");
                count++;
            }
            reader.Close();

            return count;
        }

        /// <summary>
        /// Attempts fech all field datatypes in a given database and table.
        /// Used for internal functions, may be used for getting field datatypes manually.
        /// </summary>
        /// <param name="databaseName">Selected database.</param>
        /// <param name="tableName">Selected table.</param>
        /// <returns>Returns a string list containing all field datatypes.</returns>
        /// 
        public static List<string> GetDataBaseFieldTypes(string databaseName, string tableName)
        {


            DatabaseConnection.Open();
            MySqlCommand clmn = new MySqlCommand("SHOW FIELDS FROM " + databaseName + "." + tableName + ";", DatabaseConnection);
            MySqlDataReader reader;
            string type;
            List<string> FieldTypes = new List<string>();

            reader = clmn.ExecuteReader();

            while (reader.Read())
            {
                type = reader.GetString("Type");
                FieldTypes.Add(type);
            }
            reader.Close();

            return FieldTypes;
        }

        /// <summary>
        /// Returns the connection string.
        /// </summary>
        /// <returns>Connection string.</returns>
        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Connection string needs to be set first.");

            return connectionString;
        }

        /// <summary>
        /// Sets a new connection string and returns it.
        /// </summary>
        /// <param name="url">The database address.</param>
        /// <param name="port">The database port.</param>
        /// <param name="username">Login username.</param>
        /// <param name="password">Login password.</param>
        /// <returns>New connection string.</returns>
        public static string SetConnectionString(string url, string port, string username, string password)
        {
            connectionString = $"datasource={url};port={port};username={username};password={password};";
            DatabaseConnection = new MySqlConnection(connectionString);
            Debugging.Log("SetConnectionString", $"New connection string set");
            return connectionString;
        }

        //tom pls write description of how your function works and what the arguments are, also can you make the variables more descriptive /beg
        //did
        /// <summary>
        /// Standard SQL select query. Returns a list of strings containing data from input database and table.
        /// Selects based on an input selectQuery, which denominates which elements to select (such as 'id')
        /// Selects based on an input whereQuery(condition), which sets a baseline rule for selection (such as 'where id = 1')
        /// </summary>
        /// <param name="tableName">The table name from which to select.</param>
        /// <param name="selectQuery">The SQL query of what columns to select.</param>
        /// <param name="condition">The SQL WHERE query of what rows to select.</param>
        /// <param name="databaseName">Optional name for the database (disregard the null, it's set).</param>
        /// <returns>A list of selected row strings with columns seperated by a comma (,).</returns>
        /// <example>Select("Users", "Username, Pats", "UserId = 1");</example>
        public static List<string> Select(string tableName, string selectQuery, string condition, string databaseName)
        {
            databaseName = Settings.DatabaseName;

            MySqlDataReader reader;
            MySqlDataReader reader1;

            string tarpmeme;
            string querystring;
            string tarp = "";
            int queryNumber = 0;
            int numberOfFieldslocal;

            List<string> FieldNameslocal = new List<string>();
            List<string> returno = new List<string>();
            List<string> tarplist = new List<string>();

            FieldNameslocal = GetDataBaseFields(databaseName, tableName);
            numberOfFieldslocal = GetDataBaseFieldCount(databaseName, tableName);

            DatabaseConnection.Close();
            DatabaseConnection.Open();
            //MySqlCommand clmn = new MySqlCommand($"SHOW FIELDS FROM {databaseName}.{tableName};", DatabaseConnection);

            

            if (selectQuery != "*")
            {
                tarplist = selectQuery.Split(',').ToList();
                foreach (string s in tarplist)
                {
                    queryNumber++;
                }

            }

            else
            {
                tarplist = FieldNameslocal;
                queryNumber = numberOfFieldslocal;
            }

            if (condition == "")
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT {selectQuery} FROM {databaseName}.{tableName};", DatabaseConnection);
                reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    for (int f = 0; f < queryNumber; f++)
                    {
                        if (f == queryNumber - 1)
                        {
                            querystring = "'" + tarplist[f] + "'";
                            tarpmeme = reader1.GetString(tarplist[f].ToString());
                            tarp += tarpmeme;

                        }
                        else
                        {
                            querystring = "'" + tarplist[f] + "'";
                            tarpmeme = reader1.GetString(tarplist[f].ToString());
                            tarp += tarpmeme + ",";
                        }
                    }
                    returno.Add(tarp);
                    tarp = "";
                }
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT {selectQuery} FROM {databaseName}.{tableName} WHERE {condition};", DatabaseConnection);

                reader1 = cmd.ExecuteReader();
                while (reader1.Read())
                {
                    for (int f = 0; f < queryNumber; f++)
                    {
                        if (f == queryNumber - 1)
                        {
                            querystring = "'" + tarplist[f] + "'";
                            tarpmeme = reader1.GetString(tarplist[f].ToString());
                            tarp += tarpmeme;

                        }
                        else
                        {
                            querystring = "'" + tarplist[f] + "'";
                            tarpmeme = reader1.GetString(tarplist[f].ToString());
                            tarp += tarpmeme + ",";
                        }
                    }
                    returno.Add(tarp);
                    tarp = "";
                }
            }
            DatabaseConnection.Close();
            return returno;
        }

        /// <summary>
        /// Deletes a row from selected database and table.
        /// </summary>
        /// <param name="tableName">Table from which to delete.</param>
        /// <param name="condition">Conditions for deletion.</param>
        /// <param name="databaseName">Optional name for the database (disregard the null, it's set)(AAAAAAAAAAAAAAAAAAA t. Tom).</param>
        public static void Delete(string tableName, string condition, string databaseName = null)
        {
            //databaseName = Settings.DatabaseName;

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
        /// Inserst a row into the selected database and table.
        /// </summary>
        /// <param name="tableName">Table from which to delete.</param>
        /// <param name="values">Values to be insterted.</param>
        /// <param name="databaseName">Optional name for the database (disregard the null, it's set).</param>
        public static void Insert(string databaseName, string tablename, string values)
        {
            databaseName = Settings.DatabaseName;

            MySqlDataReader reader;
            DatabaseConnection.Open();
            MySqlCommand cmd = new MySqlCommand($"Insert Into {databaseName}.{tablename} Values ({values});", DatabaseConnection);

            try
            {
                reader = cmd.ExecuteReader();
                Debugging.Log("SQL Insert", $"Inserted records into database.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debugging.Log("SQL Insert", $"Error inserting records: {e.Message}", Discord.LogSeverity.Error);
            }

            DatabaseConnection.Close();
        }

        /// <summary>
        /// Updates a row in the selected database and table.
        /// </summary>
        /// <param name="tableName">Table from which to delete.</param>
        /// <param name="values">Values to be insterted.</param>
        /// <param name="where">Selecting which rows to be affected.</param>
        /// <param name="databaseName">Optional name for the database (disregard the null, it's set).</param>
        public static void Update(string tableName, string values, string where, string databaseName = null)
        {
            //databaseName = Settings.DatabaseName;
            List<string> FieldNameslocal = new List<string>();
            MySqlDataReader reader;
            DatabaseConnection.Open();

            int numberOfFieldslocal;
            string set = "";
            int f = 1;

            FieldNameslocal = GetDataBaseFields(databaseName, tableName);
            numberOfFieldslocal = GetDataBaseFieldCount(databaseName, tableName);

            string[] zodmas = new string[numberOfFieldslocal-1];
            char[] skirt = new char[2] {',', ' ' };

            zodmas = values.Split(skirt);

            for(int i = 0; i < numberOfFieldslocal-1; i++)
            {
                set += FieldNameslocal[f].ToString() + "=";
                if (i != numberOfFieldslocal-2)
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
 

