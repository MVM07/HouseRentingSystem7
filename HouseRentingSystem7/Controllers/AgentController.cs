using HouseRentingSystem7.Core.Models.Agent;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem7.Controllers
{
    public class AgentController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            var model = new BecomeAgentFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel agent)
        {
            return RedirectToAction(nameof(HouseController.All), "House");
        }
    }
}
