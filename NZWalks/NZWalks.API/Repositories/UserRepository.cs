
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        List<StaticUser> users = new List<StaticUser>()
        {
            new StaticUser()
            {
                FirstName = "Read Only", LastName = "User", EmailAddress = "readonly@user.com",
                Id = Guid.NewGuid(), Username = "readonly@user.com", Password = "Readonly@user",
                Roles = new List<string> {"reader"}
            },
            new StaticUser()
            {
                FirstName = "Read Write", LastName = "User", EmailAddress = "readwrite@user.com",
                Id = Guid.NewGuid(), Username = "readwrite@user.com", Password = "Readwrite@user",
                Roles = new List<string> {"reader", "writer"}
            }
        };
        public async Task<StaticUser> AuthenticateAsync(string username, string password)
        {
            var user = users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);

            return user;
        }
    }
}
