using Microsoft.IdentityModel.Tokens;
using SimpleDemo.Security.Model;
using SimpleDemo.Shared.Constant;
using SimpleDemo.Shared.Exception;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleDemo.Security.Services
{
    public class JwtTokenService(JwtTokenConfig jwtTokenConfig)
        : IJwtTokenService
    {
        public string GenerateToken(List<string> permissions, Guid roleId, bool isGrpc = false)
        {
            var claims = permissions.Select(permission => new Claim(CustomClaimType.Permission, permission)).ToList();
            claims.Add(new Claim(CustomClaimType.RoleId, roleId.ToString("N")));
            claims.Add(new Claim(CustomClaimType.IsGrpc, isGrpc.ToString()));

            return GenerateToken(claims.ToArray(), DateTime.Now);
        }

        public TokenValidateResult ValidateJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) throw SimpleUnauthorizedException;

            var secret = Encoding.UTF8.GetBytes(jwtTokenConfig.Secret);
            try
            {
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };

                var principal = new JwtSecurityTokenHandler().ValidateToken(token, parameters, out var validatedToken);

                if (validatedToken is not JwtSecurityToken jwtToken) throw SimpleUnauthorizedException;

                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                    throw SimpleUnauthorizedException;

                var userName = principal.Identity?.Name;
                var role = principal.Claims.First(c => c.Type == ClaimTypes.Role).Value;
                var permissions = principal
                    .Claims.Where(c => c.Type == CustomClaimType.Permission)
                    .Select(c => c.Value)
                    .ToList();
                return new TokenValidateResult { UserName = userName, Role = role, Permissions = permissions };
            }
            catch (Exception ex) when (ExceptionFilter(ex))
            {
                throw SimpleUnauthorizedException;
            }
        }

        private string GenerateToken(Claim[]? claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                jwtTokenConfig.Issuer,
                shouldAddAudienceClaim ? jwtTokenConfig.Audience : string.Empty,
                claims,
                expires: now.AddMinutes(jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenConfig.Secret)), SecurityAlgorithms.HmacSha256));
            try
            {
                var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return accessToken;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static bool ExceptionFilter(Exception e) => e is ArgumentException or SecurityTokenException or SimpleInvalidException;

        private static readonly SimpleUnauthorizedException SimpleUnauthorizedException = new(SimpleExceptionCode.Security.InvalidToken, "The token is invalid.");
    }
}