using HouseRentingSystem7.Core.Contracts;
using HouseRentingSystem7.Core.Enumerations;
using HouseRentingSystem7.Core.Models.Agent;
using HouseRentingSystem7.Core.Models.House;
using HouseRentingSystem7.Data.Models;
using HouseRentingSystem7.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseRentingSystem7.Core.Services
{
    public class HouseService : IHouseService
    {
        private readonly IRepository repository;

        public HouseService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync()
        {
            var categories = await repository.AllReadOnly<Category>()
                .Select(c => new HouseCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return categories;
        }

        //--------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
        {
            var categoryNames = await repository.AllReadOnly<Category>()
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();

            return categoryNames;
        }

        //--------------------------------------------------------------------------------------------------

        public async Task<HouseQueryServiceModel> AllHousesAsync(string? category = null, string? searchTerm = null, HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housesPerPage = 1)
        {
            var housesToShow = repository.AllReadOnly<House>();

            if (!string.IsNullOrWhiteSpace(category))
            {
                housesToShow = housesToShow.Where(h => h.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string normalizedSearchTerm = searchTerm.ToLower();

                housesToShow = housesToShow.Where(h =>
                  h.Title.ToLower().Contains(normalizedSearchTerm) ||
                  h.Address.ToLower().Contains(normalizedSearchTerm) ||
                  h.Description.ToLower().Contains(normalizedSearchTerm));
            }

            housesToShow = sorting switch
            {
                HouseSorting.Price => housesToShow.OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesToShow.OrderBy(h => h.RenterId == null)
                .ThenByDescending(h => h.Id),
                _ => housesToShow.OrderByDescending(h => h.Id)
            };

            var houses = housesToShow
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(h => new HouseServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth
                })
                .ToList();

            var totalHouses = housesToShow.Count();

            var expectedHouses = new HouseQueryServiceModel()
            {
                TotalHousesCount = totalHouses,
                Houses = houses
            };

            return expectedHouses;
        }
        //--------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(int? agentId)
        {
            var houses = await repository.AllReadOnly<House>()
                .Where(h => h.AgentId == agentId)
                .Select(h => new HouseServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null
                })
                .ToListAsync();

            return houses;
        }

        //--------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(string userId)
        {
            var houses = await repository.AllReadOnly<House>()
                .Where(h => h.RenterId == userId)
                .Select(h => new HouseServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null
                })
                .ToListAsync();

            return houses;
        }

        //--------------------------------------------------------------------------------------------------
        public async Task<bool> CategoryExists(int categoryId)
        {
            bool categoryExists = await repository.AllReadOnly<Category>()
                .AnyAsync(c => c.Id == categoryId);

            return categoryExists;
        }
        //--------------------------------------------------------------------------------------------------
        public async Task<int> CreateAsync(HouseFormModel model, int agentId)
        {
            var house = new House()
            {
                Title = model.Title,
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                CategoryId = model.CategoryId,
                AgentId = agentId
            };

            await repository.AddAsync(house);
            await repository.SaveChangesAsync();

            return house.Id;
        }

        //--------------------------------------------------------------------------------------------------
        public async Task DeleteAsync(int houseId)
        {
            var houseToDelete = await repository.GetByIdAsync<House>(houseId);

            if (houseToDelete != null)
            {
                await repository.DeleteAsync<House>(houseToDelete.Id);
                await repository.SaveChangesAsync();
            }
        }

        //--------------------------------------------------------------------------------------------------
        public async Task EditAsync(HouseFormModel model, int id)
        {
            var houseToEdit = await repository.GetByIdAsync<House>(id);

            if (houseToEdit != null)
            {
                houseToEdit.Title = model.Title;
                houseToEdit.Address = model.Address;
                houseToEdit.Description = model.Description;
                houseToEdit.ImageUrl = model.ImageUrl;
                houseToEdit.PricePerMonth = model.PricePerMonth;
                houseToEdit.CategoryId = model.CategoryId;

                await repository.SaveChangesAsync();
            }            
        }

        //--------------------------------------------------------------------------------------------------
        public async Task<bool> ExistsAsync(int id)
        {
            bool houseExists = await repository.AllReadOnly<House>()
                .AnyAsync(h => h.Id == id);

            return houseExists;
        }

        //--------------------------------------------------------------------------------------------------
        public async Task<int> GetHouseCategoryId(int houseId)
        {
            return await repository.AllReadOnly<House>()
                .Where(h => h.Id == houseId)
                .Select(h => h.CategoryId)
                .FirstOrDefaultAsync();
        }

        //--------------------------------------------------------------------------------------------------
        public async Task<bool> HasAgentWithIdAsync(int houseId, string currentUserId)
        {
            return await repository.AllReadOnly<House>()
                 .AnyAsync(h => h.Id == houseId && h.Agent.UserId == currentUserId);
        }

        //--------------------------------------------------------------------------------------------------
        public async Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id)
        {
            var houseDetails = await repository.AllReadOnly<House>()
                .Where(h => h.Id == id)
                .Select(h => new HouseDetailsServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    Description = h.Description,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null,
                    Category = h.Category.Name,
                    Agent = new AgentServiceModel()
                    {
                        PhoneNumber = h.Agent.PhoneNumber,
                        Email = h.Agent.User.Email
                    }
                })
                .FirstOrDefaultAsync();

            return houseDetails;
        }

        //--------------------------------------------------------------------------------------------------
        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync()
        {
            var lastThreeHouses = await repository.AllReadOnly<House>()
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseIndexServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    ImageUrl = h.ImageUrl
                })
                .Take(3)
                .ToListAsync();

            return lastThreeHouses;
        }
    }
}
