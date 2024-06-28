using Microsoft.AspNetCore.Mvc;

namespace ChatDemoAPI2.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
