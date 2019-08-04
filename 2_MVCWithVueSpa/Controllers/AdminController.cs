using Microsoft.AspNetCore.Mvc;

namespace MVCWithVueSpa.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        [HttpGet("{*path}")]
        public IActionResult Index(string path) => View();
    }
}
