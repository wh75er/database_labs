using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;


public class UserDataContext : DataContext
{
    [Table(Name = "CameraBuild")]
    public class Builds 
    {
        [Column(Name = "BuildId", IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }
        [Column(Name = "CameraBodyId")]
        public int bodyId { get; set; }
        [Column(Name = "LensId")]
        public int lensId { get; set; }
        [Column(Name = "FilterId")]
        public int filterId { get; set; }
        [Column(Name = "Price")]
        public int price { get; set; }
        [Column(Name = "Year")]
        public DateTime year { get; set; }

    }

    public UserDataContext(string connectionString)
        : base(connectionString)
    {

    }

    [Table(Name = "temp")]
    public class res
    {
        [Column(Name = "MegaID", IsPrimaryKey = true)]
        public int id { get; set; }
        [Column(Name = "UltraYear")]
        public DateTime date { get; set; }
    }


    [Function(Name = "dbo.CursorThingProc")]
    public ISingleResult<res> CursorProc()
    {
        IExecuteResult objresult = this.ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())));
        ISingleResult<res> objresults = (ISingleResult<res>) objresult.ReturnValue;
        return objresults;
    }

}

namespace demo_linq
{
    class Program
    {
        #region Data

        class Employee 
        {
            public string Name { get; set; }
            public int JobID { get; set; }
            public int Age  { get; set; }
        }

        class Job
        {
            public string Name { get; set; }
            public int ID { get; set; }
        }

        [Table(Name = "CameraBuild")]
        class Builds 
        {
            [Column(Name = "BuildId", IsPrimaryKey = true, IsDbGenerated = true)]
            public int id { get; set; }
            [Column(Name = "CameraBodyId")]
            public int bodyId { get; set; }
            [Column(Name = "LensId")]
            public int lensId { get; set; }
            [Column(Name = "FilterId")]
            public int filterId { get; set; }
            [Column(Name = "Price")]
            public int price { get; set; }
            [Column(Name = "Year")]
            public DateTime year { get; set; }
    
        }

        [Table(Name = "CameraBody")]
        class Bodies 
        {
            [Column(Name = "CameraId", IsPrimaryKey = true, IsDbGenerated = true)]
            public int id { get; set; }
            [Column(Name = "Brand")]
            public string brand { get; set; }
            [Column(Name = "Model")]
            public string model { get; set; }
            [Column(Name = "Mount")]
            public string mount { get; set; }
            [Column(Name = "Megapixels")]
            public int mp { get; set; }
            [Column(Name = "Color")]
            public string color { get; set; }
    
        }

        // Specify the first data source.
        List<Job> jobs = new List<Job>()
       {
           new Job(){Name="Student", ID=001},
           new Job(){ Name="Economist", ID=002},
           new Job(){ Name="Fireman", ID=003},
           new Job() {  Name="Police Officer", ID=004},
           new Job() {  Name="Magician", ID=005}
       };

        // Specify the second data source.
        List<Employee> employees = new List<Employee>()
      {
         new Employee{Name="Jack",  JobID=001, Age=18},
         new Employee{Name="Robert",  JobID=001, Age=28},
         new Employee{Name="Ryazanova", JobID=002, Age=111},
         new Employee{Name="Kurov", JobID=002, Age=56},
         new Employee{Name="Ashley", JobID=003, Age=29},
         new Employee{Name="Chloe", JobID=003, Age=21},
         new Employee{Name="Copper", JobID=005, Age=30},
         new Employee{Name="Houdini", JobID=005, Age=221},
       };
        #endregion


        static void Main(string[] args)
        {
            Program app = new Program();
            Console.WriteLine("> Regular linq queries:\n");
            //app.RegularQueries();

            Console.WriteLine("\n> linq to xml queries:\n");
            //app.ChildXml();
            app.RenameXml();

            Console.WriteLine("\n> linq to sql queries:\n");
            //app.sqlQuery();
            //app.InsertRow();
            //app.DeleteRow();
            //app.UpdateRow();
            //app.storedProc();
        }

        void RegularQueries()
        {

            var groupJoinQuery =
                from job in jobs
                orderby job.ID
                join empl in employees on job.ID equals empl.JobID into EmplGroup 
                select new
                {
                    Job = job.Name,
                    Employees = from empl2 in EmplGroup
                               orderby empl2.Name
                               select empl2
                };

            int totalItems = 0;

            Console.WriteLine("group join:");
            foreach (var employeeGroup in groupJoinQuery)
            {
                Console.WriteLine(employeeGroup.Job);
                foreach (var emplItem in employeeGroup.Employees)
                {
                    totalItems++;
                    Console.WriteLine("  {0} {1}", emplItem.Name, emplItem.JobID);
                }
            }
            Console.WriteLine("group join: {0} items in {1} named groups\n", totalItems, groupJoinQuery.Count());

            Console.WriteLine("People catalog:\n");
            var catalog =
                from job in jobs
                orderby job.ID
                join empl in employees on job.ID equals empl.JobID into EmplGroup
                from man in EmplGroup
                select new
                {
                    Job = job.Name,
                    Name = man.Name
                };

                foreach (var info in catalog) {
                    Console.WriteLine("\tName is {0}, his(her) job is {1}", info.Name, info.Job);
                }

            Console.WriteLine("\nNested query:\n");
            var nested = 
                from empl in employees
                orderby empl.Name
                where empl.Age > 25 && empl.JobID == 
                    (
                    from job in jobs
                    where job.Name == "Fireman"
                    select job.ID
                    ).FirstOrDefault()
                select empl;

            foreach (var empl in nested) {
                Console.WriteLine("\t {0} is fireman(jobId is {1}) and his(her) age is {2} (> 25)", empl.Name, empl.JobID, empl.Age);
            }

            Console.WriteLine("\nGroupBy query:");
            var groupBy =
                from empl in employees
                group empl by empl.JobID into sg
                orderby sg.Key
                select new
                {
                    jobId = sg.Key,
                    names = from empl2 in sg
                            orderby empl2.JobID
                            select empl2
                };

            foreach (var grp in groupBy) {
                Console.WriteLine("\nJob id group {0}\n", grp.jobId);
                foreach (var employee in grp.names) {
                    Console.WriteLine("\tName:{0}, Age:{1}", employee.Name, employee.Age);
                }
            }

            Console.WriteLine("\nGroupBy with where query:");
            var groupByWhere =
                from empl in employees
                where empl.Age > 100
                group empl by empl.JobID into sg
                orderby sg.Key
                select new
                {
                    jobId = sg.Key,
                    names = from empl2 in sg
                            orderby empl2.JobID
                            select empl2
                };

            foreach (var grp in groupByWhere) {
                Console.WriteLine("\nJob id group {0}\n", grp.jobId);
                foreach (var employee in grp.names) {
                    Console.WriteLine("\tName:{0}, Age:{1}", employee.Name, employee.Age);
                }
            }

        }

        void ChildXml()
        {
            XElement root = XElement.Load("common.xml");
            IEnumerable<XElement> address =
                from el in root.Elements("Build")
                where (int)el.Element("Price") == 613
                select el;
            foreach (XElement el in address)
                Console.WriteLine(el);
        }

        void RenameXml()
        {
            XElement root = XElement.Load("common.xml");

            foreach (XElement el in root.Elements().Elements("Price")) {
                if (el.Value == "613")
                {
                    el.Value = "88005553555";
                }
            }

            root.Add(new XElement("NEW",
                new XElement("Neu", "Element")));

            root.Save("new.xml");

        }

        void sqlQuery()
        {
            string connectionString =  @"Data Source=localhost; user id=sa; password=Qwerty12; database=CameraDB";
            DataContext db = new DataContext(connectionString);
            Table<Builds> items = db.GetTable<Builds>();

            Console.WriteLine("Builds table query:");
            var Query =
                from item in items
                where item.id < 10
                select item;

            foreach (var build in Query) {
                Console.WriteLine("\t{0} {1} {2}", build.id, build.price, build.year);
            }

            Table<Bodies> bodies = db.GetTable<Bodies>();

            var Query2 =
                from item in items
                where item.id < 10
                orderby item.bodyId
                join body in bodies on item.bodyId equals body.id into buildGroup
                from body in buildGroup
                select new
                {
                    buildId = item.id,
                    bodyId = body.id,
                    brand = body.brand,
                    price = item.price
                };

            Console.WriteLine("\nBuilds and Bodies tables query:");
            foreach (var info in Query2) {
                Console.WriteLine("Body #{0} Build#{1} Brand:{2}, Price:{3}", info.bodyId, info.buildId, info.brand, info.price);
            }
        }

        void InsertRow()
        {
            string connectionString =  @"Data Source=localhost; user id=sa; password=Qwerty12; database=CameraDB";
            DataContext db = new DataContext(connectionString);
            Table<Builds> builds = db.GetTable<Builds>();

            //Insert
            Console.WriteLine("\nInserting\n" + builds.Count());
            Builds build = new Builds
            {
                bodyId = 12,
                lensId = 99,
                filterId = 99,
                price = 349,
                year = Convert.ToDateTime(DateTime.Parse("2013-09-09").ToString("M/dd/yyyy"))
            };

            builds.InsertOnSubmit(build);
            db.SubmitChanges();
        }

        void DeleteRow()
        {
            string connectionString =  @"Data Source=localhost; user id=sa; password=Qwerty12; database=CameraDB";
            DataContext db = new DataContext(connectionString);
            Table<Builds> builds = db.GetTable<Builds>();

            //Insert
            Console.WriteLine("\nDeleting\n" + builds.Count());
            Builds build = new Builds
            {
                lensId = 88,
                filterId = 88,
                price = 888,
                year = Convert.ToDateTime(DateTime.Parse("2013-09-09").ToString("M/dd/yyyy"))
            };


            builds.Attach(build);
            builds.DeleteOnSubmit(build);
            db.SubmitChanges();
        }

        void UpdateRow()
        {
            string connectionString =  @"Data Source=localhost; user id=sa; password=Qwerty12; database=CameraDB";
            DataContext db = new DataContext(connectionString);
            Table<Builds> builds = db.GetTable<Builds>();

            //Update
            Console.WriteLine("\nUpdating\n");
            var query =
                from build in builds
                where build.id == 1
                select build;

            foreach(var item in query)
            {
                item.price = 0;
            }

            db.SubmitChanges();
        }

        void storedProc()
        {
            string connectionString =  @"Data Source=localhost; user id=sa; password=Qwerty12; database=CameraDB";
            UserDataContext db = new UserDataContext(connectionString);

            
            foreach (var row in db.CursorProc()) {
                Console.WriteLine("{0} {1}", row.id, row.date);
            }
        }

    }
}
