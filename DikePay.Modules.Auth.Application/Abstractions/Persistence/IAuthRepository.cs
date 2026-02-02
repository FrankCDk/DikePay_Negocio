using DikePay.Modules.Auth.Domain;

namespace DikePay.Modules.Auth.Application.Abstractions.Persistence
{
    public interface IAuthRepository
    {
        Task<UserAccount?> GetUserByEmail(string email, CancellationToken ct = default);
        Task<MobileAuthCode?> GetActiveAuthCode(string code, CancellationToken ct = default);
        Task AddAuthCode(MobileAuthCode authCode, CancellationToken ct = default);
        Task UpdateAuthCode(MobileAuthCode authCode, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
