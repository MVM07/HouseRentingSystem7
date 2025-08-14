using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem7.Controllers
{
    public class HouseController : BaseController
    {
        public IActionResult All()
        {
            return View();
        }

        public IActionResult Mine() 
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
