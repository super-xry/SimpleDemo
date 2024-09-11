using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SimpleDemo.Application.Commerce.User.Query;
using SimpleDemo.Application.DataTransfer;
using SimpleDemo.Infrastructure.Query;
using SimpleDemo.Security.Authorization;
using SimpleDemo.Shared.Constant;

namespace SimpleDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IQueryBus queryBus) : ControllerBase
    {
        [HttpGet]
        [PermissionAuthorize(PermissionResources.User.View)]
        public async Task<IActionResult> View()
        {
            var users = await queryBus.SendAsync<ViewUserQuery, ICollection<UserDto>>(new ViewUserQuery());
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await queryBus.SendAsync<LoginQuery, UserDto>(new LoginQuery()
            {
                Email = loginRequest.Email,
                Password = loginRequest.Password
            });
            return Ok(user);
        }
    }
}