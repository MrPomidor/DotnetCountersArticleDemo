using Microsoft.AspNetCore.Mvc;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerManager _workerManager;
        public WorkerController(WorkerManager workerManager)
        {
            _workerManager = workerManager;
        }

        [HttpPost("schedule")]
        public IActionResult ScheduleWork()
        {
            _workerManager.SetupWork();
            return Ok();
        }
    }
}
