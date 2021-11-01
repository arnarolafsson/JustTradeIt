using System.Collections.Generic;
using JustTradeIt.Software.API.Models.DTOs;
using JustTradeIt.Software.API.Models.InputModels;

namespace JustTradeIt.Software.API.Repositories.Interfaces
{
    public interface ITradeRepository
    {
        IEnumerable<TradeDto> GetTrades(string email);
        TradeDetailsDto GetTradeByIdentifier(string identifier);
        IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive);
        string CreateTradeRequest(string email, TradeInputModel trade);
        void UpdateTradeRequest(string email, string identifier, Models.Enums.TradeStatus newStatus);
        IEnumerable<TradeDto> GetUserTrades(string userIdentifier);
    }
}