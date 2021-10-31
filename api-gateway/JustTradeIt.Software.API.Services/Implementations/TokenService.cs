using Amazon.Runtime.Internal;
using JustTradeIt.Software.API.Models.DTOs;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly string _secret;
        private readonly string _expDate;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(string secret, string expDate, string issuer, string audience)
        {
            _secret = secret;
            _expDate = expDate;
            _issuer = issuer;
            _audience = audience;
        }
        public string GenerateJwtToken(UserDto user)
        {
            throw new System.NotImplementedException();
        }
    }
}