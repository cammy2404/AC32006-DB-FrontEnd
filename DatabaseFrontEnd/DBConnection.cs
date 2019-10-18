using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DatabaseFrontEnd
{
    class DBConnection
    {
        private MySqlConnection connection;

        //==============================================================================================================
        // Set up the database object
        //==============================================================================================================
        public DBConnection(string server, string dbName, string user, string password)
        {
            Initialize(server, dbName, user, password);
        }


        //==============================================================================================================
        // Initialize connection with database credentials
        //==============================================================================================================
        private void Initialize(string server, string dbName, string user, string password)
        {            
            // Create connection string with the required data
            string connectionString = "SERVER=" + server + ";" 
                                    + "DATABASE=" + dbName + ";" 
                                    + "UID=" + user + ";" 
                                    + "PASSWORD=" + password + ";";

            //Console.WriteLine("Connection made with string: " + connectionString);

            // Set the connection to a new instance
            connection = new MySqlConnection(connectionString);

            // Test the cconnection by opening and closing the connection
            if (OpenConnection())
                if (CloseConnection())
                    Console.WriteLine("Connection Succesful");
        }


        //==============================================================================================================
        // Open connection to database
        //==============================================================================================================
        private bool OpenConnection()
        {
            try
            {
                // Attempt to open the connection and return result
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // Check the error number and display appropriate message
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.");
                        break;

                    case 1045:
                        Console.WriteLine("Incorrect username/password, please try again");
                        break;
                }

                return false;
            }
        }


        //==============================================================================================================
        // Close connection
        //==============================================================================================================
        private bool CloseConnection()
        {
            try
            {
                // Attempt to close the connection and return the result
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // Display the error message
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        //==============================================================================================================
        // Get connection state
        //==============================================================================================================
        public bool IsOpen()
        {
            // Return the current state of the database connection
            if (connection.State == System.Data.ConnectionState.Open) { return true; };
            return false;
        }


        /// <summary>
        /// Select specific data from a table
        /// </summary>
        /// <param name="table_name"> The name of the table to access </param>
        /// <param name="fields"> Array of column headers to get data from </param>
        /// <returns> 2-Dimentional list of data retrieved from the database </returns>
        public List<string>[] Select(string table_name, string[] fields)
        {
            if (CheckStringValid(table_name))
                return RunSelectQuery(SelectQueryBuilder(table_name, fields), fields.Length);
            return null;
        }


        //==============================================================================================================
        //
        //==============================================================================================================
        private List<string>[] RunSelectQuery(string query, int size)
        {
            List<string>[] list = new List<string>[size];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = new List<string>();
            }
            
            if (OpenConnection())
            {
                try
                {
                    MySqlDataReader dataReader = new MySqlCommand(query, connection).ExecuteReader();

                    list = new List<string>[dataReader.FieldCount];
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        list[i] = new List<string>();
                    }


                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            list[i].Add(dataReader[i] + " ");
                        }
                    }

                    dataReader.Close();

                    CloseConnection();
                    
                    return list;
                }
                catch (Exception ex)
                {
                    list = new List<string>[1];
                    list[0] = new List<string>();
                    list[0].Add(ex.Message);
                    CloseConnection();
                    return list;
                }
            }
            else
            {
                return list;
            }
        }


        //==============================================================================================================
        //==============================================================================================================
        private bool CheckStringValid(string input)
        {
            bool valid = false;

            foreach (char j in input)
            {
                int i = (int)j;
                if (i <= 122 && i >= 65)
                {
                    if (i <= 90 || i >= 97)
                    {
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                        break;
                    }
                }
                else
                {
                    valid = false;
                    break;
                }
            }

            return valid;
        }


        /// <summary>
        /// Run SELECT query on database
        /// </summary>
        /// <param name="query"> The complete query to run </param>
        /// <returns> 2-Dimentional list of data retrieved from the database </returns>
        public List<string>[] Select(string query)
        {
            string query_check = query.ToLower();
            int count = 0;

            if (query_check.Contains("select") && query_check.Contains("from"))
            {
                string[] elements = query_check.Split(' ');
                foreach (string s in elements)
                {
                    if (s == "from") break;
                    if (s != "select") count++;
                }
            }

            return RunSelectQuery(query, count);
        }

        /// <summary>
        /// Build a SELECT query using the column headers and the table name
        /// </summary>
        /// <param name="table_name"> The table to be queried </param>
        /// <param name="fields"> Array of strings containing the column headers required </param>
        /// <returns> A string containing the completed SELECT statement </returns>
        public string SelectQueryBuilder(string table_name, string[] fields)
        {
            // Start select statement
            string output = "SELECT ";

            // Add each of the fields to the statement
            foreach(string s in fields)
            {
                output += s + ", ";
            }

            // Remove the last comma
            output = output.Substring(0, output.Length - 2);

            // Add the table to select from
            output += " FROM " + table_name;

            // Return completed select statement
            return output;
        }

        /// <summary>
        /// Build a SELECT query using a single column and the table name
        /// </summary>
        /// <param name="table_name"> The table to be queried </param>
        /// <param name="field"> A string containing the column header required </param>
        /// <returns> A string containing the completed SELECT statement </returns>
        public string SelectQueryBuilder(string table_name, string field)
        {
            // Start select statement
            string output = "SELECT ";

            // Add each of the fields to the statement
            output += field + ", ";

            // Remove the last comma
            output = output.Substring(0, output.Length - 2);

            // Add the table to select from
            output += " FROM " + table_name;

            // Return completed select statement
            return output;
        }

        /// <summary>
        /// Build a SELECT query using the column headers, the table name with a WHERE clause
        /// </summary>
        /// <param name="table_name"> The table to be queried </param>
        /// <param name="fields"> Array of strings containing the column headers required </param>
        /// <param name="where"> A string containing the WHERE of the query </param>
        /// <returns> A string containing the completed SELECT statement </returns>
        public string SelectQueryBuilder(string table_name, string[] fields, string where)
        {
            // Create a where statement with the builder, then concatonate that with the where statement
            // Return the result
            return SelectQueryBuilder(table_name, fields) + " WHERE " + where;
        }

        /// <summary>
        /// Build a SELECT query using the column headers, the table name with a WHERE clause and GROUP BY a field
        /// </summary>
        /// <param name="table_name"> The table to be queried </param>
        /// <param name="fields"> Array of strings containing the column headers required </param>
        /// <param name="where"> A string containing the WHERE of the query </param>
        /// <param name="groupField"> The field header to group the data by </param>
        /// <returns> A string containing the completed SELECT statement </returns>
        public string SelectQueryBuilder(string table_name, string[] fields, string where, string groupField)
        {
            // Create a where statement with the builder, then concatonate that with the where statement
            // Return the result
            return SelectQueryBuilder(table_name, fields, where) + " GROUP BY " + groupField;
        }

        /// <summary>
        /// Build a SELECT query using the column headers, the table name with a WHERE clause and GROUP BY a field
        /// </summary>
        /// <param name="table_name"> The table to be queried </param>
        /// <param name="fields"> Array of strings containing the column headers required </param>
        /// <param name="where"> A string containing the WHERE of the query </param>
        /// <param name="groupField"> The field header to group the data by </param>
        /// <param name="ASC"> Boolean value determining if the output is sorted in acending or decending order </param>
        /// <returns> A string containing the completed SELECT statement </returns>
        public string SelectQueryBuilder(string table_name, string[] fields, string where, string groupField, bool ASC)
        {
            if (ASC)
                return SelectQueryBuilder(table_name, fields, where, groupField) + " ASC";
            else
                return SelectQueryBuilder(table_name, fields, where, groupField) + " DESC";
        }

        /// <summary>
        /// Build a SELECT query using the column headers, the table name with a WHERE clause and GROUP BY a field
        /// </summary>
        /// <param name="table_name"> The table to be queried </param>
        /// <param name="fields"> Array of strings containing the column headers required </param>
        /// <param name="groupField"> The field header to group the data by </param>
        /// <param name="ASC"> Boolean value determining if the output is sorted in acending or decending order </param>
        /// <returns> A string containing the completed SELECT statement </returns>
        public string SelectQueryBuilder(string table_name, string[] fields, string groupField, bool ASC)
        {
            string output = SelectQueryBuilder(table_name, fields) + " GROUP BY " + groupField;

            if (ASC)
                return output + " ACS";
            else
                return output + " DESC";
        }
    }
}
