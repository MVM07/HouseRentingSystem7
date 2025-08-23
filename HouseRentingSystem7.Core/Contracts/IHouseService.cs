using HouseRentingSystem7.Core.Models.House;

namespace HouseRentingSystem7.Core.Contracts
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync();

        Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync();

        Task<bool> CategoryExists(int categoryId);

        Task<int> CreateAsync(HouseFormModel model, int agentId);
    }
}
