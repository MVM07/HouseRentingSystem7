using HouseRentingSystem7.Core.Models.House;

namespace HouseRentingSystem7.Core.Contracts
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();
    }
}
