using DikePay.Modules.Auth.Application.Abstractions.Persistence;
using DikePay.Modules.Auth.Domain;

namespace DikePay.Modules.Auth.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {

        private readonly AuthDbContext _context;
        public UserRepository(AuthDbContext context) => _context = context;

        public async Task Create(UserAccount user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
