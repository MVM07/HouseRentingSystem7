namespace HouseRentingSystem7.Core.Contracts
{
    public interface IAgentService
    {
        Task<bool> ExistsById(string userId);
    }
}
