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

        Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(int? agentId);

        Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(string userId);

        Task<bool> ExistsAsync(int id);

        Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id);

        Task EditAsync(HouseFormModel model, int id);

        Task<bool> HasAgentWithIdAsync(int houseId, string currentUserId);

        Task<int> GetHouseCategoryId(int houseId);

        Task DeleteAsync(int houseId);
    }
}
