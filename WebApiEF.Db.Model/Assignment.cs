using System.Collections.Generic;

namespace WebApiEF.Db.Model
{
    public class Assignment : IPOCOClass
    {
        public int AssignmentId { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Workload> Workloads { get; set; } = new HashSet<Workload>();
    }
}