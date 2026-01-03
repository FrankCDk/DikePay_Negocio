using DikePay.Modules.Auth.Domain;

namespace DikePay.Modules.Auth.Application.Abstractions.Persistence
{
    public interface IAuthRepository
    {
        Task<UserAccount?> GetUserByEmail(string email, CancellationToken ct = default);
    }
}
