using System;

namespace WebApiEF.Db.Model
{
    public class Workload : IPOCOClass
    {
        public int WorkloadId { get; set; }
        //Browsing property, används endast i programmet och har ingen motsvarighet i databasen
        public virtual Person Person { get; set; }
        //Forein key till People tabellen
        public int PersonId { get; set; }
        //Browsing property, används endast i programmet och har ingen motsvarighet i databasen
        public virtual Assignment Assignment { get; set; }
        //Forein key till Assignments tabellen
        public int AssignmentId { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset? Stop { get; set; }
    }
}

