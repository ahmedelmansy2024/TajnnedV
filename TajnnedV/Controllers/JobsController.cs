using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Tajnned.Application.Interfaces.Hangfire;

namespace Tajnned.Api.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobsController : ControllerBase
    {
        [HttpPost("{jobName}")]
        public IActionResult Run(string jobName)
        {
            BackgroundJob.Enqueue<IJobService>(
                x => x.ExecuteAsync(jobName));

            return Ok("Job Started");
        }
    }
}
