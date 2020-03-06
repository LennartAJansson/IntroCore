using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApiEF.Db.Context;
using WebApiEF.Db.Model;

namespace WebApiEF.Controllers
{
    //This is an automatically generated controller against the dbcontext and the dbset for workloads
    [Route("api/[controller]")]
    [ApiController]
    public class WorkloadsNewController : ControllerBase
    {
        private readonly WorkloadContext context;

        public WorkloadsNewController(WorkloadContext context) => this.context = context;

        // GET: api/WorkloadsNew
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Workload>>> GetWorkloads() => await context.Workloads.ToListAsync();

        // GET: api/WorkloadsNew/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workload>> GetWorkload(int id)
        {
            var workload = await context.Workloads.FindAsync(id);

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

            context.Entry(workload).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkloadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WorkloadsNew
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Workload>> PostWorkload(Workload workload)
        {
            context.Workloads.Add(workload);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetWorkload", new { id = workload.WorkloadId }, workload);
        }

        // DELETE: api/WorkloadsNew/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Workload>> DeleteWorkload(int id)
        {
            var workload = await context.Workloads.FindAsync(id);
            if (workload == null)
            {
                return NotFound();
            }

            context.Workloads.Remove(workload);
            await context.SaveChangesAsync();

            return workload;
        }

        private bool WorkloadExists(int id) => context.Workloads.Any(e => e.WorkloadId == id);
    }
}
