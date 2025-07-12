using Microsoft.AspNetCore.Mvc;

namespace AccessoriesApp.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
