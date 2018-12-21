using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;


namespace linq
{
    class Program
    {
        #region Data

        [Table(Name = "rk3.course")]
        class courses
        {
            [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
            public int id { get; set; }
            [Column(Name = "Name")]
            public string name { get; set; }
            [Column(Name = "Date")]
            public DateTime date { get; set; }
            [Column(Name = "Spec")]
            public string spec { get; set; }
            [Column(Name = "Time")]
            public string time { get; set; }
            [Column(Name = "People")]
            public int people { get; set; }
    
        }

        [Table(Name = "rk3.employees")]
        class employees
        {
            [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
            public int id { get; set; }
            [Column(Name = "Fio")]
            public string fio { get; set; }
            [Column(Name = "Birhday")]
            public DateTime birthday { get; set; }
            [Column(Name = "Spec")]
            public string spec { get; set; }
            [Column(Name = "LastDate")]
            public DateTime lastDate { get; set; }
            [Column(Name = "courseId")]
            public int courseId { get; set; }
    
        }

        #endregion


        private readonly string connectionString = @"Data Source=localhost; database = RK3; user id = sa; password = Qwerty12";

        static void Main(string[] args)
        {
            Program app = new Program();

            app.test_query();
            app.old_employee();
        }

        void old_employee() {
            DataContext db = new DataContext(connectionString);
            Table<employees> emps = db.GetTable<employees>();
            var q = 
                from emp in emps
                where (2018-emp.lastDate.Year) > 3
                select new {
                    Name = emp.fio,
                    Spec = emp.spec
                };
            foreach (var row in q) {
                Console.WriteLine("{0}, {1}", row.Name, row.Spec);
            }
        }

        void test_query() {
            DataContext db = new DataContext(connectionString);
            Table<courses> crses = db.GetTable<courses>();
            var q = 
                from course in crses
                select course;
            foreach (var row in q) {
                Console.WriteLine("{0}, {1}", row.id, row.name);
            }
        }
    }
}
