using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem7.Core.Constants.MessageConstants;
using static HouseRentingSystem7.Infrastructure.Data.Constants.DataConstants;

namespace HouseRentingSystem7.Core.Models.House
{
    public class HouseServiceModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(HouseTitleMaxLength, MinimumLength = HouseTitleMinLength,
           ErrorMessage = LengthMessage)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(HouseAddressMaxLength, MinimumLength = HouseAddressMinLength,
          ErrorMessage = LengthMessage)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [DisplayName("Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Range(typeof(decimal), HouseRentMinValue, HouseRentMaxValue,
            ErrorMessage = "Price per month must be a positive number and less than {2} leva.")]
        [DisplayName("Price per Month")]
        public decimal PricePerMonth { get; set; }

        [DisplayName("Is Rented")]
        public bool IsRented { get; set; }
    }
}