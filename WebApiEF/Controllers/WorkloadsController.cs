using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using WebApiEF.Db.Abstract;
using WebApiEF.Db.Model;

namespace WebApiEF.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkloadsController : ControllerBase
    {
        private readonly IWorkloadService workloadService;

        public WorkloadsController(IWorkloadService workloadService) => this.workloadService = workloadService;

        //Person CRUD, Create, Read, Update, Delete
        [HttpGet("people")]
        [Produces("application/json")]
        public async Task<IEnumerable<Person>> GetPeopleAsync() => await workloadService.GetPeopleAsync();

        [HttpPost("people")]
        [Consumes("application/json")]
        public async Task<int> CreatePersonAsync([FromBody]Person person) => await workloadService.CreatePersonAsync(person);

        //[HttpPut("people")]
        //public void UpdatePerson([FromBody]Person person)
        //{

        //}

        //[HttpDelete("people")]
        //public void DeletePerson([FromBody]Person person)
        //{

        //}

        //Assignment CRUD, Create, Read, Update, Delete
        [HttpGet("assignments")]
        public async Task<IEnumerable<Assignment>> GetAssignmentsAsync() => await workloadService.GetAssignmentsAsync();

        [HttpPost("assignments")]
        public async Task<int> CreateAssignmentAsync([FromBody]Assignment assignment) => await workloadService.CreateAssignmentAsync(assignment);

        //[HttpPut("assignment")]
        //public void UpdateAssignment([FromBody]Assignment assignment)
        //{

        //}

        //[HttpDelete("assignment")]
        //public void DeleteAssignment([FromBody]Assignment assignment)
        //{

        //}


        //Workload CRUD, Create, Read, Update, Delete
        [HttpGet("{personId}&{assignmentId}")]
        public async Task<IEnumerable<Workload>> GetUnfinishedWorkloadsAsync(int personId = 0, int assignmentId = 0) => await workloadService.GetUnfinishedWorkloadsAsync(personId, assignmentId);

        [HttpPost("{personId}&{assignmentId}&{comment}")]
        public async Task<int> StartWorkloadAsync(int personId, int assignmentId, string comment) => await workloadService.StartWorkloadAsync(personId, assignmentId, comment, DateTimeOffset.UtcNow);

        [HttpPut("{workloadId}")]
        public async Task StopWorkloadAsync(int workloadId) => await workloadService.StopWorkloadAsync(workloadId, DateTimeOffset.UtcNow);

        //[HttpDelete()]
        //public void DeleteWorkload([FromBody]Workload workload)
        //{

        //}
    }
}
