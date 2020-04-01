using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebApiEF.Db.Abstract;
using WebApiEF.Db.Model;

namespace WebApiEF.Controllers
{
    //This is an automatically generated controller against the dbcontext and the dbset for workloads
    [Route("api/[controller]")]
    [ApiController]
    public class WorkloadsNewController : ControllerBase
    {
        private readonly IWorkloadService service;

        public WorkloadsNewController(IWorkloadService service) => this.service = service;

        // GET: api/WorkloadsNew
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Workload>> GetWorkloads() => await service.GetUnfinishedWorkloadsAsync(0, 0);

        // GET: api/WorkloadsNew/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workload>> GetWorkload(int id)
        {
            var workloads = await service.GetUnfinishedWorkloadsAsync(0, 0);
            var workload = workloads.Where(x => x.WorkloadId == id).FirstOrDefault();
            if (workload == null)
            {
                return NotFound();
            }

            return workload;
        }

        // PUT: api/WorkloadsNew/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkload(int id, Workload workload)
        {
            if (id != workload.WorkloadId)
            {
                return BadRequest();
            }

            await service.StopWorkloadAsync(id, DateTimeOffset.UtcNow);

            return NoContent();
        }

        // POST: api/WorkloadsNew
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Workload>> PostWorkload(Workload workload)
        {
            int i = await service.StartWorkloadAsync(workload.PersonId, workload.AssignmentId, workload.Comment, DateTimeOffset.UtcNow);

            return CreatedAtAction("GetWorkload", new { id = i }, workload);
        }
    }
}
