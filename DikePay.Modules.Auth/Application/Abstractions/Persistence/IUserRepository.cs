using DikePay.Modules.Auth.Domain;

namespace DikePay.Modules.Auth.Application.Abstractions.Persistence
{
    public interface IUserRepository
    {
        Task Create(UserAccount user);
    }
}
