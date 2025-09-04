using HouseRentingSystem7.Attributes;
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
        public async Task<IActionResult> All([FromQuery]AllHousesQueryModel query)
        {
            var queryResult = await houseService.AllHousesAsync(query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.HousesPerPage);

            query.TotalHousesCount = queryResult.TotalHousesCount;
            query.Houses = queryResult.Houses;

            var houseCategories = await houseService.AllCategoryNamesAsync();
            query.Categories = houseCategories;

            return View(query);
        }

        //----------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Mine() 
        {
            IEnumerable<HouseServiceModel> myHouses = null;

            var userId = User.UserId();

            if (await agentService.ExistsByIdAsync(userId))
            {
                int? currentAgentId = await agentService.GetAgentIdAsync(userId);

                myHouses = await houseService.AllHousesByAgentId(currentAgentId);
            }
            else
            {
                myHouses = await houseService.AllHousesByUserId(userId);
            }

            return View(myHouses);
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
        public async Task<IActionResult> Edit(int id)
        {
            if (await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithIdAsync(id, User.UserId()) == false)
            {
                return Unauthorized();
            }

            var house = await houseService.HouseDetailsByIdAsync(id);

            var houseCategoryId = await houseService.GetHouseCategoryId(house.Id);

            var houseModel = new HouseFormModel()
            {
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                CategoryId = houseCategoryId,
                Categories = await houseService.AllCategoriesAsync()
            };

            return View(houseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseFormModel model)
        {
            if (await houseService.ExistsAsync(id) == false)
            {
                return View();
            }

            if (await houseService.HasAgentWithIdAsync(id, User.UserId()) == false)
            {
                return Unauthorized();
            }

            if (await houseService.CategoryExists(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category doesn't exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await houseService.AllCategoriesAsync();

                return View(model);
            }

            await houseService.EditAsync(model, id);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        //----------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithIdAsync(id, User.UserId()) == false)
            {
                return Unauthorized();
            }

            var house = await houseService.HouseDetailsByIdAsync(id);

            var model = new HouseDetailsViewModel()
            {
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel model)
        {
            if (await houseService.ExistsAsync(model.Id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithIdAsync(model.Id, User.UserId()) == false)
            {
                return Unauthorized();
            }

            await houseService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }

        //----------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Details(int id)
        {
            if (await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            var model = await houseService.HouseDetailsByIdAsync(id);

            return View(model);
        }

        //----------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await agentService.ExistsByIdAsync(User.UserId()))
            {
                return Unauthorized();
            }

            if (await houseService.IsRentedAsync(id))
            {
                return BadRequest();
            }

            await houseService.RentAsync(id, User.UserId());

            return RedirectToAction(nameof(All));
        }

        //----------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.IsRentedAsync(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.IsRentedByUserWithIdAsync(id, User.UserId()) == false)
            {
                return Unauthorized();
            }

            await houseService.LeaveAsync(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
