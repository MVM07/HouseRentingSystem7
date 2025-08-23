using HouseRentingSystem7.Core.Constants;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem7.Data.Constants.DataConstants;

namespace HouseRentingSystem7.Core.Models.Agent
{
    public class BecomeAgentFormModel
    {
        [Required(ErrorMessage = MessageConstants.RequiredMessage)]
        [StringLength(AgentPhoneNumberMaxLength, MinimumLength = AgentPhoneNumberMinLength,
            ErrorMessage = MessageConstants.LengthMessage)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
