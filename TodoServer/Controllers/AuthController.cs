using Microsoft.AspNetCore.Mvc;

namespace TodoServer.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
