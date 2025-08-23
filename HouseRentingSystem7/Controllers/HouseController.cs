﻿using HouseRentingSystem7.Attributes;
using HouseRentingSystem7.Core.Contracts;
using HouseRentingSystem7.Core.Models.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HouseRentingSystem7.Controllers
{
    public class HouseController : BaseController
    {
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;

        public HouseController(IHouseService _houseService, IAgentService _agentService)
        {
            houseService = _houseService;
            agentService = _agentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = new AllHousesQueryModel();

            return View(model);
        }

        //----------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Mine() 
        {
            var model = new AllHousesQueryModel();

            return View(model);
        }

        //----------------------------------------------------------------------------------------------------

        [HttpGet]
        [MustBeAgent]
        public async Task<IActionResult> Add()
        {          
            var model = new HouseFormModel()
            {
                Categories = await houseService.AllCategoriesAsync()
            };            

            return View(model);
        }
        
        [HttpPost]
        [MustBeAgent]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (await houseService.CategoryExists(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category doesn't exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await houseService.AllCategoriesAsync();

                return View(model);
            }

            int? agentId = await agentService.GetAgentIdAsync(User.UserId());

            var houseId = await houseService.CreateAsync(model, agentId ?? 0);

            return RedirectToAction(nameof(Details), new {id = houseId});
        }

        //----------------------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new HouseFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseFormModel model)
        {
            return RedirectToAction(nameof(Details), new { id = "1" });
        }

        //----------------------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = new HouseDetailsViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel model)
        {
            return RedirectToAction(nameof(All));
        }

        //----------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Details(int id)
        {
            var model = new HouseDetailsViewModel();

            return View(model);
        }

        //----------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

        //----------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
