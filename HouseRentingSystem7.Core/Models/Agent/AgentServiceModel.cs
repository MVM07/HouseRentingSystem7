using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem7.Core.Models.Agent
{
    public class AgentServiceModel
    {
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
