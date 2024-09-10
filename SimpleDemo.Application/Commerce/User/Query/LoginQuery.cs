using SimpleDemo.Infrastructure.Query;

namespace SimpleDemo.Application.Commerce.User.Query
{
    public class LoginQuery : IQuery
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}