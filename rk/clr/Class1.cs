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
            public SqlInt32 Count;
            public SqlInt32 People;
    
            public ResultRow(SqlInt32 people_, SqlInt32 count_)
            {
                People = people_;
                Count = count_;
            }
        }

        [SqlFunction(
        DataAccess = DataAccessKind.Read,
        FillRowMethodName = "FillRow",
        TableDefinition = "Count int, People int")]  
        public static IEnumerable InitMethod()  
        {  
            ArrayList results = new ArrayList();

            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();

                using (SqlCommand select = new SqlCommand(
                    "select count(*), MAX(t.People) from rk3.teachers t " +
                    "JOIN rk3.students s ON s.teacherId = t.id " +
                    "GROUP BY s.teacherId",
                    connection))
                {
                    using (SqlDataReader reader = select.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new ResultRow(
                                reader.GetSqlInt32(0),  // String
                                reader.GetSqlInt32(1)  // String
                            ));
                        }
                    }
                }
            }
            return results;
        }  

        public static void FillRow(Object obj, out SqlInt32 Count, out SqlInt32 People)  
        {  
            ResultRow selectResults = (ResultRow)obj;

            Count = selectResults.Count;
            People = selectResults.People;
        }  
    }
}
