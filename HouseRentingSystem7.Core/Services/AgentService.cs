using HouseRentingSystem7.Core.Contracts;
using HouseRentingSystem7.Data.Models;
using HouseRentingSystem7.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem7.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository repository;

        public AgentService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateAsync(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            await repository.AddAsync(agent);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string userId)
        {
            var agent = await repository.AllReadOnly<Agent>()
                .AnyAsync(a => a.UserId == userId);

            return agent;
        }

        public async Task<int?> GetAgentIdAsync(string userId)
        {
            var agent = (await repository.AllReadOnly<Agent>()
                .FirstOrDefaultAsync(a => a.UserId == userId))?.Id;

            return agent;
        }

        public async Task<bool> UserHasRentsAsync(string userId)
        {
            var userWithRents = await repository.AllReadOnly<House>()
                .AnyAsync(h => h.RenterId == userId);

            return userWithRents;
        }

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
        {
            var userWithPhone = await repository.AllReadOnly<Agent>()
                 .AnyAsync(a => a.PhoneNumber == phoneNumber);

            return userWithPhone;
        }
    }
}
