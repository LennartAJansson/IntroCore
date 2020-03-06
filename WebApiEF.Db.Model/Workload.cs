using System;

namespace WebApiEF.Db.Model
{
    public class Workload : IPOCOClass
    {
        public int WorkloadId { get; set; }
        public virtual Person Person { get; set; }
        public int PersonId { get; set; }
        public virtual Assignment Assignment { get; set; }
        public int AssignmentId { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset? Stop { get; set; }
    }
}

