using HouseRentingSystem7.Core.Contracts;
using HouseRentingSystem7.Infrastructure.Data.Common;

namespace HouseRentingSystem7.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository repository;

        public AgentService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> ExistsById(string userId)
        {
            
        }
    }
}
