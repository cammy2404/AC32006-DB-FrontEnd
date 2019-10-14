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

        public DBConnection(string server, string dbName, string user, string password)
        {
            Initialize(server, dbName, user, password);
        }

        //Initialize values
        private void Initialize(string server, string dbName, string user, string password)
        {            
            string connectionString = "SERVER=" + server + ";" 
                                    + "DATABASE=" + dbName + ";" 
                                    + "UID=" + user + ";" 
                                    + "PASSWORD=" + password + ";";

            Console.WriteLine("Connection made with string: " + connectionString);

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection opened");
                return true;
            }
            catch (MySqlException ex)
            {
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
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

        //Close connection
        private bool CloseConnection()
        {
            
            try
            {
                connection.Close();
                Console.WriteLine("Connection closed");
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Get connection state
        public bool IsOpen()
        {
            if (connection.State == System.Data.ConnectionState.Open) { return true; };
            return false;
        }

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM People";

            //Create a list to store the result
            List<string>[] list = new List<string>[4];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    for(int i = 0; i < dataReader.FieldCount; i++)
                    {
                        list[i].Add(dataReader[i] + " ");
                    }

                    //list[0].Add(dataReader[0] + " ");
                    //list[1].Add(dataReader[1] + " ");
                    //list[2].Add(dataReader[2] + " ");
                    //list[3].Add(dataReader[3] + " ");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

    }
}
