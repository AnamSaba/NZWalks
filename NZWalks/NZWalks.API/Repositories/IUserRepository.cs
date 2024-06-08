using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        Task<StaticUser> AuthenticateAsync(string username, string password);
    }
}
