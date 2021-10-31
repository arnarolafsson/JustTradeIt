using System.Linq;
using System.Text;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

/*
 * Code Inspired by arnarleifs
 * URL: https://github.com/arnarleifs/WebServicesRU/blob/master/net-core/secret-service/RU.WebServices.SecretService.API/Middlewares/JwtAuthenticationMiddleware.cs
 */
namespace JustTradeIt.Software.API.Middlewares
{
    public static class JwtAuthenticationMiddleware
    {
        public static AuthenticationBuilder AddJwtTokenAuthentication(this AuthenticationBuilder builder,
            IConfiguration config)
        {
            var jwtConfig = config.GetSection("JwtConfig");
            var secret = jwtConfig.GetSection("secret").Value;
            var audience = jwtConfig.GetSection("audience").Value;
            var issuer = jwtConfig.GetSection("issuer").Value;
            var key = Encoding.ASCII.GetBytes(secret);
            builder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    NameClaimType = "name"
                };
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var claim = context.Principal.Claims.FirstOrDefault(c => c.Type == "tokenId")?.Value;
                        int.TryParse(claim, out var tokenId);
                        var jwtTokenService = context.HttpContext.RequestServices.GetService<IJwtTokenService>();

                        if (jwtTokenService.IsTokenBlacklisted(tokenId))
                        {
                            context.Response.StatusCode = 401;
                            await context.Response.WriteAsync("JWT token provided is invalid!");
                        }
                    }
                };
            });
            return builder;
        }
    }
}