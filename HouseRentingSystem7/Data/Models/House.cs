using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem7.Data.Constants.DataConstants;

namespace HouseRentingSystem7.Data.Models
{
    public class House
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(HouseTitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(HouseAddressMaxLength)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(HouseDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public decimal PricePerMonth { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        [Required]
        public int AgentId { get; set; }

        public Agent Agent { get; set; } = null!;

        public string RenterId { get; set; } = string.Empty;
    }
}