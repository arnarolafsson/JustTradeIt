using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace JustTradeIt.Software.API.Middlewares
{
    public static class JwtAuthenticationMiddleware
    {
        public static AuthenticationBuilder AddJwtTokenAuthentication(this AuthenticationBuilder builder,
            IConfiguration config)
        {
            var jwtConfig = config.GetSection("JwtConfig");
        }
    }
}