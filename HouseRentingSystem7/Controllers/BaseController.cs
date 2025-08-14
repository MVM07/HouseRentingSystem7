using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentingSystem7.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public string GetUserId()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Not found";

            return userId;
        }
    }
}
