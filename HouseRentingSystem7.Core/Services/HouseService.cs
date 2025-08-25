using HouseRentingSystem7.Core.Contracts;
using HouseRentingSystem7.Core.Enumerations;
using HouseRentingSystem7.Core.Models.House;
using HouseRentingSystem7.Data.Models;
using HouseRentingSystem7.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;

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
