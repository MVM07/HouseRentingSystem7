using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem7.Data.Constants.DataConstants;

namespace HouseRentingSystem7.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<House> Houses { get; set; } = new List<House>();
    }
}
