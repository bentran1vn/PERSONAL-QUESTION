using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bentran1vn.question.repository.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var info = $"Service is running normally on {_webHostEnvironment.EnvironmentName}...";
            return Ok(info);
        }
    }
}
