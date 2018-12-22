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

            cls.bad_student();
            cls.best_choice();
        }

        void bad_student() 
        {
            string queryString = @"select * from rk3.students where teacherId IS NULL";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand dataQueryCommand = new SqlCommand(queryString, connection);
            connection.Open();

            SqlDataReader dataReader = dataQueryCommand.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine("Id:{0}\tName:{1}, Spec:{2}", dataReader.GetValue(0), dataReader.GetValue(1), dataReader.GetValue(2));
            }


            connection.Close();
        }

        void best_choice()
        {
            string queryString = @"select * from rk3.students s
                    JOIN rk3.teachers t ON s.Spec = t.Spec
                    where s.teacherId is NULL and t.People < ALL(
                    select count(*) as amount from rk3.students
                    group by teacherId
                )";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand dataQueryCommand = new SqlCommand(queryString, connection);

            connection.Open();
            SqlDataReader dataReader = dataQueryCommand.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine("Id:{0}\tName:{1}, Spec:{2}", dataReader.GetValue(0), dataReader.GetValue(1), dataReader.GetValue(2));
            }


            connection.Close();
        }

    }
}
