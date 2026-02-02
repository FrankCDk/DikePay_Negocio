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

        #region
        public async Task<MobileAuthCode?> GetActiveAuthCode(string code, CancellationToken ct = default)
        {
            return await _context.MobileAuthCodes
                .Include(x => x.User) // Senior Tip: Traemos el usuario en la misma consulta
                .FirstOrDefaultAsync(x => x.Code == code &&
                                         !x.IsUsed &&
                                         x.ExpiryDate > DateTime.UtcNow, ct);
        }

        public async Task AddAuthCode(MobileAuthCode authCode, CancellationToken ct = default)
        {
            await _context.MobileAuthCodes.AddAsync(authCode, ct);
        }

        public async Task UpdateAuthCode(MobileAuthCode authCode, CancellationToken ct = default)
        {
            _context.MobileAuthCodes.Update(authCode);
            // No guardamos cambios aquí para que el Handler decida cuándo hacer el Commit
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }
        #endregion


    }
}
