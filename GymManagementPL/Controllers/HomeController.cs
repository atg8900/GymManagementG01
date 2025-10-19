using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    //BaseURL/Home/Index
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
