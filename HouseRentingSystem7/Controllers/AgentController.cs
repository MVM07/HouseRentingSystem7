using HouseRentingSystem7.Attributes;
using HouseRentingSystem7.Core.Constants;
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
        [NotAnAgent]
        public IActionResult Become()
        {
            var model = new BecomeAgentFormModel();

            return View(model);
        }

        [HttpPost]
        [NotAnAgent]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            var userId = User.UserId();

            if (await agentService.UserWithPhoneNumberExistsAsync(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), MessageConstants.PhoneExistsMessage);
            }

            if (await agentService.UserHasRentsAsync(userId))
            {
                ModelState.AddModelError("Error", MessageConstants.RentsMessage);
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
