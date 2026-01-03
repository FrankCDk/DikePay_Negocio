using DikePay.Modules.Auth.Application.Abstractions.Persistence;

namespace DikePay.Modules.Auth.Infrastructure.Persistence
{
    public class AuthUnitOfWork : IAuthUnitOfWork
    {
        private readonly AuthDbContext _context;

        public AuthUnitOfWork(AuthDbContext context) => _context = context;

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _context.SaveChangesAsync(ct);
    }
}
