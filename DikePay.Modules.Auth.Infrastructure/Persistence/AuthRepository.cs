using DikePay.Modules.Auth.Application.Abstractions.Persistence;
using DikePay.Modules.Auth.Domain;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Auth.Infrastructure.Persistence
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _context;
        public AuthRepository(AuthDbContext context) => _context = context;

        public async Task<UserAccount?> GetUserByEmail(string email, CancellationToken ct = default)
        {
            return await _context.Users
                .AsNoTracking() // Siempre en búsquedas de lectura
                .FirstOrDefaultAsync(u => u.Email == email, ct);
        }
    }
}
