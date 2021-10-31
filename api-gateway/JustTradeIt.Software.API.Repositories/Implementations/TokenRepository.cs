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
            throw new System.NotImplementedException();
        }

        public void VoidToken(int tokenId)
        {
            throw new System.NotImplementedException();
        }
    }
}