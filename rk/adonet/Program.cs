using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace dotnet
{
    class Program
    {
        private readonly string connectionString = @"Data Source=localhost; database = RK3; user id = sa; password = Qwerty12";

        static void Main(string[] args)
        {
            Program cls = new Program();

            cls.test_query();
            cls.old_employee();

        }

        void old_employee() 
        {
            string queryString = @"select * from rk3.course";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand dataQueryCommand = new SqlCommand(queryString, connection);
            connection.Open();

            SqlDataReader dataReader = dataQueryCommand.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine("Id:{0}\tName:{1}", dataReader.GetValue(0), dataReader.GetValue(1));
            }


            connection.Close();
        }

        public void test_query()
        {

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
                Console.WriteLine("Connection properties:");
                Console.WriteLine("\tDatabase:          {0}", connection.Database);
                Console.WriteLine("\tData Source:       {0}", connection.DataSource);
                Console.WriteLine("\tServer version:    {0}", connection.ServerVersion);
                Console.WriteLine("\tConnection state:  {0}\n", connection.State);
            connection.Close();
        }

    }
}
