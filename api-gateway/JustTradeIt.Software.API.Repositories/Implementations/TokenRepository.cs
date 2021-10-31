using System.Linq;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Interfaces;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private readonly TradeDbContext _dbContext;

        public TokenRepository(TradeDbContext DbContext)
        {
            _dbContext = DbContext;
        }
        public JwtToken CreateNewToken()
        {
            var newtoken = new JwtToken();
            _dbContext.JwtTokens.Add(newtoken);
            _dbContext.SaveChanges();
            return newtoken;

        }

        public bool IsTokenBlacklisted(int tokenId)
        {
            var token = _dbContext.JwtTokens.FirstOrDefault(t => t.Id == tokenId);
            if (token == null) return true;
            return token.blacklisted;
        }

        public void VoidToken(int tokenId)
        {
            var token = _dbContext.JwtTokens.FirstOrDefault(t => t.Id == tokenId);
            if (token == null) return;

            token.blacklisted = true;
            _dbContext.SaveChanges();
        }
    }
}