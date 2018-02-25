using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace sqlmemes
{
    class SqlFuncts
    {
        public static string constring = "datasource=localhost;port=3306;username=root;password=password;";
        public static MySqlConnection conDB = new MySqlConnection(constring);
        public static List<string> names = new List<string>();
        public static List<string> types = new List<string>();

        public static int n = 0;
        //public static MySqlCommand cmd = new MySqlCommand("SELECT * FROM test.example WHERE id = 1;", conDB);

        public void testfunct()
        {
            MySqlCommand check = new MySqlCommand("SELECT * FROM dbname.users", conDB);
            MySqlDataReader reader;
            conDB.Open();
            reader = check.ExecuteReader();
            while (reader.Read())
            {

                Console.WriteLine(reader.GetInt32("id") + " " + reader.GetString("name"));
            }
            conDB.Close();
        }

        public void SetConString(string src, string prt, string usrnm, string pw)
        {
            constring = "datasource=" + src.ToString() + ";port=" + prt.ToString() + ";username=" + usrnm.ToString() + ";password=" + pw.ToString() + ";";
            Console.WriteLine("New connection string settings saved : ");
            Console.WriteLine(constring);
        }

        public List<string> Select(string select, string dbname, string tablename, string where)
        {
            MySqlDataReader reader;
            MySqlDataReader reader1;
            
            List<string> memes = new List<string>();

            string tarpmeme;
            string querystring;
            string name;
            string tarp = "";
            string type;

            List<string> returno = new List<string>();

            conDB.Open();
            MySqlCommand clmnname = new MySqlCommand("SELECT column_name from information_schema.columns where table_schema = '" + dbname.ToString() + "' and table_name = '" + tablename.ToString() + "';", conDB);
            MySqlCommand clmn = new MySqlCommand("SHOW FIELDS FROM "+ dbname +"."+ tablename +";", conDB);

            reader = clmn.ExecuteReader();

            while (reader.Read())
            {
                name = reader.GetString("Field");
                names.Add(name);
                type = reader.GetString("Type");
                types.Add(type);
                n++;
            }
            reader.Close();

            if (where == "")
            {
                MySqlCommand cmd = new MySqlCommand("SELECT " + select.ToString() + " FROM " + dbname.ToString() + "." + tablename.ToString() + ";", conDB);
                reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    for (int f = 0; f < n; f++)
                    {
                        querystring ="'" + names[f] + "'";
                        tarpmeme = reader1.GetString(names[f].ToString());
                        tarp += tarpmeme + ",";
                    }
                    returno.Add(tarp);
                    tarp = "";
                }
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("SELECT " + select.ToString() + " FROM " + dbname.ToString() + "." + tablename.ToString() + " WHERE " + where.ToString() + ";", conDB);
                
                reader1 = cmd.ExecuteReader();
                while (reader1.Read())
                {
                    for (int f = 0; f < n; f++)
                    {
                        querystring = "'" + names[f] + "'";
                        tarpmeme = reader1.GetString(names[f].ToString());
                        tarp += tarpmeme + ",";
                    }
                    returno.Add(tarp);
                    tarp = "";
                }
            }
            conDB.Close();
            return returno;
        }
        public void Delete(string dbname, string tablename, string condition)
        {
            MySqlDataReader reader;
            conDB.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM " + dbname + "." + tablename + " WHERE " + condition.ToString() + ";", conDB);
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            conDB.Close();
        }

        public void Insert(string dbname, string tablename, string values)
        {
            MySqlDataReader reader;
            conDB.Open();
            MySqlCommand cmd = new MySqlCommand("Insert Into " + dbname + "." + tablename + " Values (" + values.ToString() + ");", conDB);
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            conDB.Close();
        }

        public void Update(string dbname, string tablename, string values, string where)
        {
            MySqlDataReader reader;
            conDB.Open();
            string set = "";
            int f = 1;
            //List<string> zodlist = new List<string>();

            string[] zodmas = new string[n-1];
            char[] skirt = new char[2] {',', ' ' };

            zodmas = values.Split(skirt);

            /*foreach(string s in names)
            {
                Console.WriteLine(s);
            }*/
            for(int i = 0; i < n-1; i++)
            {
                set += names[f].ToString() + "=";
                if (i != n-2)
                {
                    set += zodmas[i] + ",";
                }
                else
                {
                    set += zodmas[i];
                }
                f++;
            }
            //Console.WriteLine("set = " + set.ToString());
            //Console.WriteLine("UPDATE " + dbname + "." + tablename + " SET " + set.ToString() + " WHERE " + where.ToString() + ";");
            MySqlCommand cmd = new MySqlCommand("UPDATE " + dbname + "." + tablename + " SET " + set.ToString() + " WHERE " + where.ToString() + ";", conDB);
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            conDB.Close();
        }

    }
}
 

