using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Lab9.NET
{
    class Tasks
    {
        private readonly string connectionString = @"Data Source=localhost; database = CameraDB; user id = sa; password = Qwerty12";

        static void Main(string[] args)
        {
            Tasks solution = new Tasks();

            ///solution.connectedObjects_task_1_ConnectionString();
            ///solution.connectedObjects_task_2_SimpleScalarSelection();
            ///solution.connectedObjects_task_3_SqlCommand_SqlDataReader();
            ///solution.connectedObjects_task_4_SqlCommandWithParameters();
            ///solution.connectedObjects_task_5_SqlCommand_StoredProcedure();
            ///solution.disconnectedObjects_task_6_DataSetFromTable();
            ///solution.disconnectedObjects_task_7_FilterSort();
            ///solution.disconnectedObjects_8_Insert();
            ///solution.disconnectedObjects_9_Delete();
            solution.disconnectedObjects_10_Xml();
        }

        public void connectedObjects_task_1_ConnectionString()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 1, "[Connected] Shows connection info.");

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection properties:");
                Console.WriteLine("\tDatabase:          {0}", connection.Database);
                Console.WriteLine("\tData Source:       {0}", connection.DataSource);
                Console.WriteLine("\tServer version:    {0}", connection.ServerVersion);
                Console.WriteLine("\tConnection state:  {0}", connection.State);
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the connection creating. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
            }
            Console.ReadLine();
        }

        public void connectedObjects_task_2_SimpleScalarSelection()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 2, "[Connected] Simple scalar query.");

            string queryString = @"select count(*) from CameraBuild";
            SqlConnection connection = new SqlConnection(connectionString);
            
            SqlCommand scalarQueryCommand = new SqlCommand(queryString, connection);
            Console.WriteLine("Sql command \"{0}\" has been created.", queryString);
            try
            {
                connection.Open();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
                Console.WriteLine("Amount of Builds: {0}", scalarQueryCommand.ExecuteScalar());
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
            }
            Console.ReadLine();
        }

        public void connectedObjects_task_3_SqlCommand_SqlDataReader()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 3, "[Connected] DataReader for query.");

            string queryString = @"select * from CameraBody where cameraId < 11";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand dataQueryCommand = new SqlCommand(queryString, connection);
            try
            {
                connection.Open();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
                SqlDataReader dataReader = dataQueryCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    Console.WriteLine("\tId:{0}\tModel:{1}", dataReader.GetValue(0), dataReader.GetValue(2));
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
            }
            Console.ReadLine();
        }

        public void connectedObjects_task_4_SqlCommandWithParameters()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 4, "[Connected] SqlCommand (Insert, Delete).");

            string preferenceQuery = @"select body.Brand, body.Model, body.Megapixels, body.Color, build.Price " + 
                                        "from CameraBody as body " + 
                                        "join CameraBuild build ON build.CameraBodyId = body.CameraId " +
                                        "where build.Price < @price and body.Brand=@brand and body.Color=@color";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand preferenceQueryCommand = new SqlCommand(preferenceQuery, connection);

            //parameters
            preferenceQueryCommand.Parameters.Add("@price", SqlDbType.Int);
            preferenceQueryCommand.Parameters.Add("@brand", SqlDbType.VarChar, 20);
            preferenceQueryCommand.Parameters.Add("@color", SqlDbType.VarChar, 20);

            try
            {
                connection.Open();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);

                Console.WriteLine("We'll find the best body for your preferences:");
                Console.Write("Your price red line: ");
                int price = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\nAvaible options: {Sigma, Canon, Olympus, Panasonic" +
                            ", Ricoh, Fujifilm, Pentax, Vageeswari, Praktica, Rollei, Epson, " +
                                        "AgfaPhoto, Medion, V-Lux, Minox, Casio, etc.}");
                Console.Write("Brand: ");
                string brand = Console.ReadLine();
                Console.WriteLine("\nAvaible options: {pink, yellow, gray, " +
                                "silver, brown, blue, white, purple, red, black}");
                Console.Write("color:");
                string color = Console.ReadLine();

                preferenceQueryCommand.Parameters["@price"].Value = price;
                preferenceQueryCommand.Parameters["@brand"].Value = brand;
                preferenceQueryCommand.Parameters["@color"].Value = color;

                SqlDataReader dataReader = preferenceQueryCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    Console.WriteLine("\tBrand:{0}\tModel:{1}\tmp:{2}\tColor:{3}\tPrice:{4}", 
                        dataReader.GetValue(0), dataReader.GetValue(1), dataReader.GetValue(2)
                                                    , dataReader.GetValue(3), dataReader.GetValue(4));
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
            }
            Console.ReadLine();
        }

        public void connectedObjects_task_5_SqlCommand_StoredProcedure()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 5, "[Connected] Stored procedure.");

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand storedProcedureCommand = connection.CreateCommand();
            storedProcedureCommand.CommandType = CommandType.StoredProcedure;
            storedProcedureCommand.CommandText = "WHAT_DB_IS_THAT";

            try
            {
                connection.Open();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);

                Console.Write("Input table name: ");
                string tb_name = Console.ReadLine();
                storedProcedureCommand.Parameters.Add("tb_name", SqlDbType.VarChar, 128).Value = tb_name;

                var result = storedProcedureCommand.ExecuteReader();

                while (result.Read())
                {
                    Console.WriteLine("\tindexId:{0}\tobject_id:{1}\tname:{2}", 
                        result.GetValue(0), result.GetValue(1), result.GetValue(2));
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
            }
            Console.ReadLine();
        }

        public void disconnectedObjects_task_6_DataSetFromTable()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 6, "[Disconnected] DataSet from the table.");

            string query = @"select Year, Price from CameraBuild where Price > 1400 order by Year";

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet cameraBuild = new DataSet();
                dataAdapter.Fill(cameraBuild, "CameraBuild");
                DataTable table = cameraBuild.Tables["CameraBuild"];

                Console.WriteLine("Price more than 1400$:");
                foreach (DataRow row in table.Rows) {
                    Console.WriteLine("year:{0}\tprice:{1}", row["Year"], row["Price"]);
                }
                Console.WriteLine();
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql query execution. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
            }
            Console.ReadLine();
        }

        public void disconnectedObjects_task_7_FilterSort()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 7, "[Disconnected] Filter and sort.");

            string query = @"select * from CameraBody";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "CameraBody");
                DataTableCollection tables = dataSet.Tables;

                Console.Write("Which brand do you need? Input brand name: ");
                string brand = Console.ReadLine();
                Console.WriteLine();

                string select_query = "Brand like '%" + brand + "%'";
                foreach (DataRow row in tables["CameraBody"].Select(select_query))
                {
                    Console.Write("{0} ", row["Brand"]);
                    Console.Write("{0} ", row["Model"]);
                    Console.Write("{0} ", row["Color"]);
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql query execution. Message: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
            }
            Console.ReadLine();
        }

        public void disconnectedObjects_8_Insert()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 8, "[Disconnected] Insert.");

            string dataCommand = @"select * from Filter";
            string insertQueryString = @"insert into Filter(Name, Purpose, Diameter) values (@name, @purpose, @diameter)";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);

                Console.WriteLine("Inserting a new filter: ");
                Console.Write("Input name:");
                string name = Console.ReadLine();
                Console.Write("Input purpose:");
                string purpose = Console.ReadLine();
                Console.Write("Input diameter:");
                int diameter = Convert.ToInt32(Console.ReadLine());

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataCommand, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Filter");
                DataTable table = dataSet.Tables["Filter"];

                DataRow insertingRow = table.NewRow();
                insertingRow["Name"] = name;
                insertingRow["Purpose"] = purpose;
                insertingRow["Diameter"] = diameter;

                table.Rows.Add(insertingRow);

                Console.WriteLine("Filters");
                foreach (DataRow row in table.Rows)
                {
                    Console.Write("{0} ", row["Name"]);
                    Console.Write("{0} ", row["Purpose"]);
                    Console.Write("{0} ", row["Diameter"]);
                    Console.WriteLine();
                }
                
                SqlCommand insertQueryCommand = new SqlCommand(insertQueryString, connection);
                insertQueryCommand.Parameters.Add("@name", SqlDbType.VarChar, 50, "Name");
                insertQueryCommand.Parameters.Add("@purpose", SqlDbType.VarChar, 50, "Purpose");
                insertQueryCommand.Parameters.Add("@diameter", SqlDbType.Int, 50, "Diameter");

                dataAdapter.InsertCommand = insertQueryCommand;
                dataAdapter.Update(dataSet, "Filter");
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
            }
            Console.ReadLine();
        }

        public void disconnectedObjects_9_Delete()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 9, "[Disconnected] Delete.");

            string dataCommand = @"select * from Filter";
            string deleteQueryString = @"delete from Filter where Name = @name";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
                Console.WriteLine("Deleting filter: ");
                Console.Write("Input filter name: ");
                string name = Console.ReadLine();
                
                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataCommand, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Filter");
                DataTable table = dataSet.Tables["Filter"];

                string filter = "Name = '" + name + "'";
                foreach (DataRow row in table.Select(filter))
                {
                    row.Delete();
                }

                SqlCommand deleteQueryCommand = new SqlCommand(deleteQueryString, connection);
                deleteQueryCommand.Parameters.Add("@name", SqlDbType.VarChar, 50, "Name");

                dataAdapter.DeleteCommand = deleteQueryCommand;
                dataAdapter.Update(dataSet, "Filter");

                Console.WriteLine("Filters");
                foreach (DataRow row in table.Rows)
                {
                    Console.Write("{0} ", row["Name"]);
                    Console.Write("{0} ", row["Purpose"]);
                    Console.Write("---- {0}\n", row["Diameter"]);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
            }
            Console.ReadLine();
        }

        public void disconnectedObjects_10_Xml()
        {
            Console.WriteLine("".PadLeft(80, '-'));
            Console.WriteLine("Task #{0}: {1}", 10, "WriteXml.");

            string query = @"select * from CameraBuild";

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "CameraBuild");
                DataTable table = dataSet.Tables["CameraBuild"];

                dataSet.WriteXml("CameraBuild.xml");
                Console.WriteLine("\nXml has been written\n");
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql query execution. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("\n\tConnection state:  {0}\n", connection.State);
            }
            Console.ReadLine();
        }
    }
}
