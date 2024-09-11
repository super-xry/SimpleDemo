using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SimpleDemo.Security.Authorization;
using SimpleDemo.Security.Model;
using SimpleDemo.Security.Services;
using SimpleDemo.Shared.Constant;
using SimpleDemo.Shared.Exception;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SimpleDemo.Security.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AuthorizationExtension
    {
        public static void AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtTokenConfig = configuration.GetSection("JwtTokenConfig").Get<JwtTokenConfig>();
            if (jwtTokenConfig == null)
            {
                throw new SimpleNotFoundException("", "JwtTokenConfig not found.");
            }
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = GetTokenValidationParameters(jwtTokenConfig);
                options.Events = GetJwtBearerEvents;
            });

            services.UsePermissionService(jwtTokenConfig);
            services.AddAuthorization();
        }

        public static void UsePermissionService(this IServiceCollection services, JwtTokenConfig jwtTokenConfig)
        {
            services.AddSingleton(jwtTokenConfig);

            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationRequirement>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
        }

        private static TokenValidationParameters GetTokenValidationParameters(JwtTokenConfig jwtTokenConfig) => new()
        {
            ValidateIssuer = true,
            ValidIssuer = jwtTokenConfig.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
            ValidAudience = jwtTokenConfig.Audience,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1),
            NameClaimType = CustomClaimType.Name,
            RoleClaimType = CustomClaimType.RoleId
        };

        private static JwtBearerEvents GetJwtBearerEvents => new()
        {
            OnMessageReceived = context =>
            {
                if (!string.IsNullOrEmpty(context.Token) || !context.HttpContext.Request.Path.StartsWithSegments("/hubs"))
                {
                    return Task.CompletedTask;
                }

                var accessToken = context.Request.Query["access_token"];

                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    }
}