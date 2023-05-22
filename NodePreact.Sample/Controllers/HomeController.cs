using Microsoft.AspNetCore.Mvc;

namespace NodePreact.Sample.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/{**all}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}