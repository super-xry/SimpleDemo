using Microsoft.EntityFrameworkCore;
using SimpleDemo.Application.Commerce.User.Query;
using SimpleDemo.Application.DataTransfer;
using SimpleDemo.Domain.DbEntity.CommerceEntity;
using SimpleDemo.Infrastructure.Query;

namespace SimpleDemo.Application.Commerce.User.Handler
{
    public class UserQueryHandler(Func<ICommerceRepository> repositoryFactory)
        : IQueryHandler<UserQuery, Task<ICollection<UserDto>>>
    {
        public async Task<ICollection<UserDto>> HandleAsync(UserQuery query, CancellationToken cancellationToken)
        {
            using var repository = repositoryFactory();
            var users = await repository.Users.Select(it => new UserDto()
            {
            }).ToListAsync(cancellationToken);

            return users;
        }
    }
}