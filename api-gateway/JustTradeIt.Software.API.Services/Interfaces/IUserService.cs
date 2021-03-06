using System.Collections.Generic;
using JustTradeIt.Software.API.Models.DTOs;

namespace JustTradeIt.Software.API.Services.Interfaces
{
    public interface IUserService
    {
         IEnumerable<TradeDto> GetUserTrades(string userIdentifier);
         UserDto GetUserInformation(string identifier);
    }
}