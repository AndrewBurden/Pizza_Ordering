using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;

namespace StockManagement
{
    class Item
    {
        private string name;
        private string unitName;
        private double currentLevel;
        private double lowLevel;

        public int validateInput(string n, string u, string c, string l)
        {
            name = n;
            unitName = u;
            if (!Double.TryParse(c, out currentLevel) || currentLevel < 0)
            {
                return 1;
            }
            else if (!Double.TryParse(l, out lowLevel) || lowLevel < 0)
            {
                return 2;
            }
            if (checkExistance())
            {
                return 3;
            }
            else
            {
                saveItem();
            }
            return 0;

        }


        public bool checkExistance()
        {
            string connStr = "server=csshrpt.eku.edu;user=csc834;database=csc834;port=3306;password=CSC834student;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM changitemtable WHERE name = @name";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return false;
        }
        public void saveItem()
        {
            //Remember to add MySql.Data as a reference into your project.

            //Add "using MySql.Data.MySqlClient;" to the beginning of the code.
            
            string connStr = "server=csshrpt.eku.edu;user=csc834;database=csc834;port=3306;password=CSC834student;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {    
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "INSERT INTO changitemtable (name, unitname, currentlevel, lowlevel) VALUES (@name, @unitname, @currentlevel, @lowlevel)";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@unitname", unitName);
                cmd.Parameters.AddWithValue("@currentlevel", currentLevel);
                cmd.Parameters.AddWithValue("@lowlevel", lowLevel);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {  
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }

        public static ArrayList getItemList()
        {
            ArrayList newList = new ArrayList();

            DataTable myTable = new DataTable();  
            string connStr = "server=csshrpt.eku.edu;user=csc834;database=csc834;port=3306;password=CSC834student;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                //retrieve all the conflicted events from the database
                string sql = "SELECT * FROM changitemtable ORDER BY name ASC";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable);
                Console.WriteLine("Table is ready.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            //convert the retrieved data to Event objects and save them in the event list, if any
            foreach (DataRow row in myTable.Rows)
            {
                Item newItem = new Item();
                newItem.name = row["name"].ToString();
                newItem.unitName = row["unitname"].ToString();
                newItem.currentLevel = Double.Parse(row["currentlevel"].ToString());
                newItem.lowLevel = Double.Parse(row["lowlevel"].ToString());
                newList.Add(newItem);
            }
            // if the event list is NOT empty, it means that at least one conflicted event exists, then return the first conflicted event
            if (newList.Count == 0)
            {
                Console.WriteLine("The list is empty!");
                return null;
            }
            return newList;
        }

        public string getName()
        {
            return name;
        }

        public string getUnitName()
        {
            return unitName;
        }

        public double getCurrentLevel()
        {
            return currentLevel;
        }

        public double getLowLevel()
        {
            return lowLevel;
        }

    };
}
