using System.Collections.Generic;

namespace WebApiEF.Db.Model
{
    //POCO - Plain Old CLR Object
    public class Person : IPOCOClass
    {
        public int PersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Postalcode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Workload> Workloads { get; set; } = new HashSet<Workload>();
    }
}
