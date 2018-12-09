using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Text; //stringbuffer
using System.Collections.Generic; //list
using System.Collections;

namespace scalar_func
{
    public class Class1
    {
        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlInt32 scal_func(SqlInt32 num)
        {
            return num+200;
        }

    }


    [Serializable]
    [SqlUserDefinedAggregate(
        Format.Native,
        IsInvariantToDuplicates = false,
        IsInvariantToNulls = true,
        IsInvariantToOrder = true,
        IsNullIfEmpty = true,
        Name = "PriceAvg")]
    public struct PriceAvg
    {
        private long sum;
        private int count;

        public void Init()
        {
            sum = 0;
            count = 0;
        }

        public void Accumulate(SqlInt32 Value, SqlInt32 Price)
        {
            if (!Value.IsNull && !Price.IsNull)
            {
                sum += (long)Value * (long)Price;
                count += (int)Price;
            }
        }

        public void Merge(PriceAvg Group)
        {
            sum += Group.sum;
            count += Group.count;
        }

        public SqlInt32 Terminate()
        {
            if (count > 0)
            {
                int value = (int)(sum / count);
                return new SqlInt32(value);
            }
            else
            {
                return SqlInt32.Null;
            }
        }
    }

    public class TabularPriceLog
    {  

        private class ResultRow
        {
            public SqlInt32 BuildId;
            public SqlInt32 Price;
    
            public ResultRow(SqlInt32 buildid_, SqlInt32 price_)
            {
                BuildId = buildid_;
                Price = price_;
            }
        }

        [SqlFunction(
        DataAccess = DataAccessKind.Read,
        FillRowMethodName = "FillRow",
        TableDefinition = "BuildId int" +
                          "Price int")]  
        public static IEnumerable InitMethod()  
        {  
            ArrayList results = new ArrayList();

            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();

                using (SqlCommand select = new SqlCommand(
                    "SELECT TOP 100 BuildId, Price FROM CameraBuild",
                    connection))
                {
                    using (SqlDataReader reader = select.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new ResultRow(
                                reader.GetSqlInt32(0),  // CustId
                                reader.GetSqlInt32(1)  // Name
                            ));
                        }
                    }
                }
            }
            return results;
        }  

        public static void FillRow(Object obj, out SqlInt32 BuildId, out SqlInt32 Price)  
        {  
            ResultRow selectResults = (ResultRow)obj;

            BuildId = selectResults.BuildId;
            Price = selectResults.Price;
        }  
    }

    public partial class StoredProcedure
    {
        [Microsoft.SqlServer.Server.SqlProcedure]
        public static void getOldBuilds ()
        {
            SqlContext.Pipe.Send("This is my CLR SP");
            SqlConnection con = new SqlConnection("context connection=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from CameraBuild where year([Year]) < 2009", con);
            SqlDataReader reader = cmd.ExecuteReader();
            SqlContext.Pipe.Send(reader);
        }
    }

    public class Trigger
    {
        [SqlTrigger(Name = @"CameraBuild", Target = "[dbo].[Users]", Event = "FOR INSERT, DELETE")]
        public static void cameraBuild()
        {
            SqlCommand command;
            SqlTriggerContext triggContext = SqlContext.TriggerContext;
            SqlPipe pipe = SqlContext.Pipe;
            SqlDataReader reader;

            switch (triggContext.TriggerAction)
            {
                case TriggerAction.Insert:
                    SqlContext.Pipe.Send("You inserted something!");
                    break;

                case TriggerAction.Delete:
                    using (SqlConnection connection= new SqlConnection(@"context connection=true"))
                    {
                        connection.Open();
                        command = new SqlCommand(@"SELECT * FROM DELETED;",connection);
                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            pipe.Send(@"You deleted the following rows:");
                            while (reader.Read())
                            {
                                pipe.Send(@"'" + reader.GetInt32(0).ToString()
                                + @"', '" + reader.GetInt32(1).ToString() + @"'");
                            }

                            reader.Close();
                        }
                        else
                        {
                            pipe.Send("No rows affected.");
                        }
                    }

                    break;
            }
        }
    }
}
