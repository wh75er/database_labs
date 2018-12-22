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

        [Table(Name = "rk3.teachers")]
        class teachers
        {
            [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
            public int tid { get; set; }
            [Column(Name = "Name")]
            public string tname { get; set; }
            [Column(Name = "Spec")]
            public string tspec { get; set; }
            [Column(Name = "People")]
            public int people { get; set; }
    
        }

        [Table(Name = "rk3.students")]
        class students 
        {
            [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
            public int id { get; set; }
            [Column(Name = "Name")]
            public string name { get; set; }
            [Column(Name = "Birthday")]
            public DateTime birthday { get; set; }
            [Column(Name = "Spec")]
            public string spec { get; set; }
            [Column(Name = "courseTheme")]
            public string theme { get; set; }
            [Column(Name = "teacherId")]
            public Nullable<int> teacherId { get; set; }
    
        }

        #endregion


        private readonly string connectionString = @"Data Source=localhost; database = RK3; user id = sa; password = Qwerty12";

        static void Main(string[] args)
        {
            Program app = new Program();

            //app.test_query();
            app.bad_student();
            app.best_choice();
        }

        void bad_student() {
            DataContext db = new DataContext(connectionString);
            Table<students> students = db.GetTable<students>();
            var q = 
                from student in students
                where (student.teacherId == null)
                select new {
                    Name = student.name,
                    Spec = student.spec
                };
            foreach (var row in q) {
                Console.WriteLine("{0}, {1}", row.Name, row.Spec);
            }
        }

        void best_choice() {
            DataContext db = new DataContext(connectionString);
            Table<students> students = db.GetTable<students>();
            Table<teachers> teachers = db.GetTable<teachers>();

            Console.WriteLine("Best choice:");

            var q =  
                from student in students
                where student.teacherId == null
                join teacher in teachers on student.spec equals teacher.tspec into joined
                from a in joined
                select new {
                    tname = a.tname,
                    name = student.name,
                    spec = a.tspec
                }
                ;
            
            foreach (var row in q) {
                Console.WriteLine("Student name: {0}, Teacher name: {1}, Spec: {2}" , row.name, row.tname, row.spec);
            }
        }

        void test_query() {
            DataContext db = new DataContext(connectionString);
            Table<students> students = db.GetTable<students>();
            var q = 
                from student in students 
                select student;
            foreach (var row in q) {
                Console.WriteLine("{0}, {1}, {2}", row.id, row.name, row.teacherId);
            }
        }
    }
}
