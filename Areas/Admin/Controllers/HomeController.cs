using Microsoft.AspNetCore.Mvc;

namespace YESHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("{area}/{controller}/{action}/{id?}")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
