using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem7.Data.Constants.DataConstants;

namespace HouseRentingSystem7.Core.Models.Agent
{
    public class BecomeAgentFormModel
    {
        [Required]
        [StringLength(AgentPhoneNumberMaxLength, MinimumLength = AgentPhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
