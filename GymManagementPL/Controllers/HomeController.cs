using GymManagementDAL.Entities;
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

        //BaseURL/Home/Trainers
        public JsonResult Trainers()
        {
            var trainers = new List<Trainer>() 
            { 
                new Trainer(){Name="abdullah" , Phone="0106325600"},
                new Trainer(){Name="aya" , Phone="0106325656"},
            };
            return Json( trainers);
        }

        public RedirectResult Redirect()
        {
            return Redirect("https://www.linkedin.com/");
        }

        public ContentResult GetContent()
        {
            return Content("<h1>Hello From Gold's Gym</h1>","text/html");
        }

        public FileResult DownloadFile()
        {
            var pathFile = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot" ,"css" ,"site.css");
            var fileBytes = System.IO.File.ReadAllBytes(pathFile);
            return File(fileBytes , "text/css" ,"AtgStyle.css");
        }
    }
}
