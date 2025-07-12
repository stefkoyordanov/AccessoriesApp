using Microsoft.AspNetCore.Mvc;

namespace AccessoriesApp.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
