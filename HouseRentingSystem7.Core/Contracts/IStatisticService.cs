using HouseRentingSystem7.Core.Models.Statistics;

namespace HouseRentingSystem7.Core.Contracts
{
    public interface IStatisticService
    {
        Task<StatisticServiceModel> Total();
    }
}
