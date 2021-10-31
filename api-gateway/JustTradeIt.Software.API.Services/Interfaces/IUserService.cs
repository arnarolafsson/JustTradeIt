using System.Collections.Generic;

namespace JustTradeIt.Software.API.Services.Interfaces
{
    public interface IUserService
    {
         IEnumerable<TradeDto> GetUserTrades(string userIdentifier);
         UserDto GetUserInformation(string identifier);
    }
}