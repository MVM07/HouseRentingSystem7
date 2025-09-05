using HouseRentingSystem7.Core.Contracts;

namespace HouseRentingSystem7.Core.Models.House
{
    public class HouseIndexServiceModel : IHouseModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
