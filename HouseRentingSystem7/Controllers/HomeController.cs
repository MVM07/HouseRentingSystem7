using System.Diagnostics;
using HouseRentingSystem7.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem7.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
