using HouseRentingSystem7.Core.Contracts;
using HouseRentingSystem7.Core.Models.Statistics;
using HouseRentingSystem7.Data.Models;
using HouseRentingSystem7.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem7.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IRepository repository;

        public StatisticService(IRepository _repository)
        {
            repository = _repository;   
        }

        public async Task<StatisticServiceModel> Total()
        {
            int totalHouses = await repository.AllReadOnly<House>()
                .CountAsync();
            int totalRents = await repository.AllReadOnly<House>()
                .Where(h => !string.IsNullOrWhiteSpace(h.RenterId))
                .CountAsync();

            var houseStatistics = new StatisticServiceModel()
            {
                TotalHouses = totalHouses,
                TotalRents = totalRents
            };

            return houseStatistics;
        }
    }
}
