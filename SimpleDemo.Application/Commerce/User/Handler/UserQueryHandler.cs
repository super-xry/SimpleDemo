﻿using Microsoft.EntityFrameworkCore;
using SimpleDemo.Application.Commerce.User.Query;
using SimpleDemo.Application.DataTransfer;
using SimpleDemo.Application.Events;
using SimpleDemo.Domain.DbEntity.CommerceEntity;
using SimpleDemo.Infrastructure.Events;
using SimpleDemo.Infrastructure.Query;
using SimpleDemo.Shared.Constant;
using SimpleDemo.Shared.Exception;

namespace SimpleDemo.Application.Commerce.User.Handler
{
    public class UserQueryHandler(Func<ICommerceRepository> repositoryFactory, IEventPublisher eventPublisher)
        : IQueryHandler<ViewUserQuery, Task<ICollection<UserDto>>>,
            IQueryHandler<LoginQuery, Task<UserDto>>
    {
        public async Task<ICollection<UserDto>> HandleAsync(ViewUserQuery query, CancellationToken cancellationToken)
        {
            using var repository = repositoryFactory();
            var users = await repository.Users.Select(it => new UserDto()
            {
                UserName = it.Name,
                NickName = it.NickName,
                Role = it.Role.Name
            }).ToListAsync(cancellationToken);

            return users;
        }

        public async Task<UserDto> HandleAsync(LoginQuery query, CancellationToken cancellationToken)
        {
            using var repository = repositoryFactory();
            var user = await repository.Users.FirstOrDefaultAsync(it => it.Email == query.Email && it.Password == query.Password, cancellationToken);
            await eventPublisher.PublishAsync(new LoginEvent()
            {
                From = query.Email,
                UserId = user?.Id
            }, cancellationToken);

            if (user == null)
            {
                throw new SimpleNotFoundException(SimpleExceptionCode.Application.UserNotFound, "User not found~.");
            }

            var role = await repository.Roles.FirstOrDefaultAsync(it => it.Id == user.RoleId, cancellationToken);

            return new UserDto()
            {
                UserName = user.Name,
                Role = role?.Name
            };
        }
    }
}