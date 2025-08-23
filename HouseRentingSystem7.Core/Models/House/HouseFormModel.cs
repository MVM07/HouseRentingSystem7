using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem7.Infrastructure.Data.Constants.DataConstants;
using static HouseRentingSystem7.Core.Constants.MessageConstants;

namespace HouseRentingSystem7.Core.Models.House
{
    public class HouseFormModel
    {
        [Required]
        [StringLength(HouseTitleMaxLength, MinimumLength = HouseTitleMinLength,
            ErrorMessage = LengthMessage)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(HouseAddressMaxLength, MinimumLength = HouseAddressMinLength,
            ErrorMessage = LengthMessage)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(HouseDescriptionMaxLength, MinimumLength = HouseDescriptionMinLength,
            ErrorMessage = LengthMessage)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Range(typeof(decimal), HouseRentMinValue, HouseRentMaxValue, 
            ErrorMessage = "Price per month must be a positive number and less than {2} leva.")]
        [Display(Name = "Price per month")]
        public decimal PricePerMonth { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; } = new List<HouseCategoryServiceModel>();
    }
}
