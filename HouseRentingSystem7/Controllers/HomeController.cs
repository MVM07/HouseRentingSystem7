using System.Diagnostics;
using HouseRentingSystem7.Core.Contracts;
using HouseRentingSystem7.Core.Models.Home;
using HouseRentingSystem7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem7.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHouseService houseService;

        public HomeController(IHouseService _houseService)
        {
            houseService = _houseService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var houses = await houseService.LastThreeHousesAsync();

            return View(houses);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return View("Error400");
            }
            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}
