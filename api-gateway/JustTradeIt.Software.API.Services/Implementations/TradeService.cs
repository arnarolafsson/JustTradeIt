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
        private readonly IQueueService _queueService;
        private readonly IUserService _userService;

        public TradeService(ITradeRepository tradeRepository, IQueueService queueService, IUserService userService)
        {
            _tradeRepository = tradeRepository;
            _queueService = queueService;
            _userService = userService;
        }
        public string CreateTradeRequest(string email, TradeInputModel tradeRequest)
        {
            var request = _tradeRepository.CreateTradeRequest(email, tradeRequest);
            var tradeInfo = _tradeRepository.GetTradeByIdentifier(request);
            _queueService.PublishMessage("new-trade-request", tradeInfo);
            return request;
        }

        public TradeDetailsDto GetTradeByIdentifer(string tradeIdentifier)
        {
            return _tradeRepository.GetTradeByIdentifier(tradeIdentifier);
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive = true)
        {
            return _tradeRepository.GetTradeRequests(email, onlyIncludeActive);
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            return _tradeRepository.GetTrades(email);
        }

        public void UpdateTradeRequest(string identifier, string email, string status)
        {
            switch(status)
            {
                case "Accepted":
                    _tradeRepository.UpdateTradeRequest(identifier, email, TradeStatus.Accepted);
                    _queueService.PublishMessage("trade-update-request", _tradeRepository.GetTradeByIdentifier(identifier));
                    break;
                case "Declined":
                    _tradeRepository.UpdateTradeRequest(identifier, email, TradeStatus.Declined);
                    _queueService.PublishMessage("trade-update-request", _tradeRepository.GetTradeByIdentifier(identifier));
                    break;
                case "Cancelled":
                    _tradeRepository.UpdateTradeRequest(identifier, email, TradeStatus.Cancelled);
                    _queueService.PublishMessage("trade-update-request", _tradeRepository.GetTradeByIdentifier(identifier));
                    break;
                default:
                    _tradeRepository.UpdateTradeRequest(identifier, email, TradeStatus.TimedOut);
                    _queueService.PublishMessage("trade-update-request", _tradeRepository.GetTradeByIdentifier(identifier));
                    break;
            }
            
        }
    }
}