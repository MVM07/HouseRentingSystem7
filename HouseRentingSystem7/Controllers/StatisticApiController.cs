using HouseRentingSystem7.Core.Contracts;
using HouseRentingSystem7.Core.Models.Statistics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HouseRentingSystem7.Controllers
{
    [ApiController]
    [Route("api/statistic")]
    public class StatisticApiController : ControllerBase
    {
        private readonly IStatisticService statisticService;

        public StatisticApiController(IStatisticService _statisticService)
        {
            statisticService = _statisticService;
        }

        [HttpGet]
        public async Task<StatisticServiceModel> GetStatistic()
        {
            return await statisticService.Total();
        }
    }
}
