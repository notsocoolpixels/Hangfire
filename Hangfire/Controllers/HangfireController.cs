using KI.Jobs._3DmodelSync;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {

        public readonly MyApplication _myApplication;
        private readonly ILogger<HangfireController> _logger;
        public HangfireController(MyApplication myApplication, ILogger<HangfireController> logger)
        {
            _myApplication = myApplication;
            _logger = logger;

        }

        [HttpPost("welcome")]
        public IActionResult Welcome(string userName)
        {
            var jobId = BackgroundJob.Enqueue(() => SendWelcomeMail(userName));
            return Ok($"Job Id {userName} Completed. Welcome Mail Sent!");
        }

       [NonAction]
        public  void SendWelcomeMail(string userName)
        {
            //Logic to Mail the user
           // Console.WriteLine($"Welcome to our application, {userName}");
        }

        [HttpGet("Start")]
        public IActionResult StartApplication()
        {
            // var jobId = BackgroundJob.Enqueue(() => _myApplication.Run());

            _logger.LogInformation("hello ");
             RecurringJob.AddOrUpdate(() => _myApplication.Run(), Cron.Hourly());

            return Ok();

        }
        

    }
}
