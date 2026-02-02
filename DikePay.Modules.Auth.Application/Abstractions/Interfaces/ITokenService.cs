using DikePay.Modules.Auth.Domain;

namespace DikePay.Modules.Auth.Application.Abstractions.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(UserAccount user);
    }
}
