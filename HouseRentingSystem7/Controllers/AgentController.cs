using HouseRentingSystem7.Core.Contracts;
using HouseRentingSystem7.Core.Models.Agent;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentingSystem7.Controllers
{
    public class AgentController : BaseController
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService _agentService)
        {
            agentService = _agentService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            if (await agentService.ExistsByIdAsync(User.UserId()))
            {
                return BadRequest();
            }
            var model = new BecomeAgentFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            var userId = User.UserId();

            if (await agentService.ExistsByIdAsync(userId))
            {
                return BadRequest();
            }

            if (await agentService.UserWithPhoneNumberExistsAsync(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Phone number already exists.");
            }

            if (await agentService.UserHasRentsAsync(userId))
            {
                ModelState.AddModelError("Error", "You should have no rents to become an agent.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await agentService.CreateAsync(userId, model.PhoneNumber);

            return RedirectToAction(nameof(HouseController.All), "House");
        }
    }
}
