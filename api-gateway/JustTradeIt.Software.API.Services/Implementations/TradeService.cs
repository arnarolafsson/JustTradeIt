using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JustTradeIt.Software.API.Models.DTOs;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;

        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }
        public string CreateTradeRequest(string email, TradeInputModel tradeRequest)
        {
            return _tradeRepository.CreateTradeRequest(email, tradeRequest);
        }

        public TradeDetailsDto GetTradeByIdentifer(string tradeIdentifier)
        {
            return _tradeRepository.GetTradeByIdentifier(tradeIdentifier);
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            return _tradeRepository.GetTrades(email);
        }

        public void UpdateTradeRequest(string identifier, string email, string status)
        {
            TradeStatus statusEnum;
            switch(status)
            {
                case "Accepted":
                    _tradeRepository.UpdateTradeRequest(identifier, email, TradeStatus.Accepted);
                    break;
                case "Declined":
                    _tradeRepository.UpdateTradeRequest(identifier, email, TradeStatus.Declined);
                    break;
                case "Cancelled":
                    _tradeRepository.UpdateTradeRequest(identifier, email, TradeStatus.Cancelled);
                    break;
                default:
                    _tradeRepository.UpdateTradeRequest(identifier, email, TradeStatus.TimedOut);
                    break;
            }
            
        }
    }
}