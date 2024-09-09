using Microsoft.AspNetCore.Mvc;
using SimpleDemo.Application.Commerce.User.Query;
using SimpleDemo.Application.DataTransfer;
using SimpleDemo.Infrastructure.Query;

namespace SimpleDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IQueryBus queryBus) : ControllerBase
    {
        [HttpGet]
        public async Task<ICollection<UserDto>> View()
        {
            var users = await queryBus.SendAsync<UserQuery, ICollection<UserDto>>(new UserQuery());
            return users;
        }
    }
}