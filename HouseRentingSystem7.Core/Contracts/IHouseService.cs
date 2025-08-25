using HouseRentingSystem7.Core.Enumerations;
using HouseRentingSystem7.Core.Models.House;
using HouseRentingSystem7.Data.Models;

namespace HouseRentingSystem7.Core.Contracts
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync();

        Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync();

        Task<bool> CategoryExists(int categoryId);

        Task<int> CreateAsync(HouseFormModel model, int agentId);

        Task<HouseQueryServiceModel> AllHousesAsync(string? category = null, 
            string? searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1);

        Task<IEnumerable<string>> AllCategoryNamesAsync();
    }
}
