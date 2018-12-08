using System;
using System.Data.SqlTypes;
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

    public class TableFunction
    {

        [SqlFunction(FillRowMethodName = "GenerateIntervalFillRow")]
        public static IEnumerable GenerateInterval(SqlInt32 From, SqlInt32 To)
        {
            int[] items = new int[To.Value - From.Value + 1];
            for (int i = From.Value; i <= To.Value; i++)
                items[i - From.Value] = i;

            return items;
        }

        public static void GenerateIntervalFillRow(object o, out SqlInt32 item)
        {
            item = new SqlInt32((int)o);
        }
    }
}
