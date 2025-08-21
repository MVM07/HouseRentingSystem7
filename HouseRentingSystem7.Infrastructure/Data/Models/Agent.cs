using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem7.Data.Constants.DataConstants;

namespace HouseRentingSystem7.Data.Models
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class Agent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AgentPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        public IdentityUser User { get; set; } = null!;

        public List<House> Houses { get; set; } = new List<House>();
    }
}