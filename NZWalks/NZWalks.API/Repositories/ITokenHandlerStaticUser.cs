using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface ITokenHandlerStaticUser
    {
        Task<string> CreateTokenAsync(StaticUser user);
    }
}
