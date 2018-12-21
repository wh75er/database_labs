using System;
using System.IO;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Text; //stringbuffer
using System.Collections.Generic; //list
using System.Collections;

namespace table_func      
{
    public class Tabular
    {  

        private class ResultRow
        {
            public SqlString Name;
    
            public ResultRow(SqlString name_)
            {
                Name = name_;
            }
        }

        [SqlFunction(
        DataAccess = DataAccessKind.Read,
        FillRowMethodName = "FillRow",
        TableDefinition = "Name string")]  
        public static IEnumerable InitMethod()  
        {  
            ArrayList results = new ArrayList();

            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();

                using (SqlCommand select = new SqlCommand(
                    "SELECT e.Fio " + 
                    "FROM rk3.employees e " +
                    "JOIN rk3.course c ON c.id = e.courseId " + 
                    "WHERE c.Name = 'RTDM Administration'",
                    connection))
                {
                    using (SqlDataReader reader = select.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new ResultRow(
                                reader.GetSqlString(0)  // String
                            ));
                        }
                    }
                }
            }
            return results;
        }  

        public static void FillRow(Object obj, out SqlString Name)  
        {  
            ResultRow selectResults = (ResultRow)obj;

            Name = selectResults.Name;
        }  
    }
}
