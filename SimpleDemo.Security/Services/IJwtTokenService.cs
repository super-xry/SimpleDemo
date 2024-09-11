using SimpleDemo.Security.Model;

namespace SimpleDemo.Security.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(List<string> permissions, Guid roleId, bool isGrpc = false);

        TokenValidateResult ValidateJwtToken(string token);
    }
}