using System;
using System.Collections.Generic;
using System.Linq;
using JustTradeIt.Software.API.Models.DTOs;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Interfaces;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class TradeRepository : ITradeRepository
    {
        private readonly TradeDbContext _dbContext;

        public TradeRepository(TradeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string CreateTradeRequest(string email, TradeInputModel trade)
        {
            var sender = _dbContext.Users.FirstOrDefault(i => i.Email == email);
            var receiver = _dbContext.Users.FirstOrDefault(i => i.PublicIdentifier == trade.ReceiverIdentifier);
            if (receiver == null) { throw new Exception("Receiver not found!!"); }
            
            var tradeRequest = new Trade
            {
                PublicIdentifier = Guid.NewGuid().ToString(),
                IssueDate = DateTime.Now,
                ModifiedByDate = DateTime.Now,
                ModifiedBy = sender.FullName,
                TradeStatus = (int)TradeStatus.Pending,
                ReceiverId = receiver.Id,
                SenderId = sender.Id
            };
            return "";
        }

        public TradeDetailsDto GetTradeByIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public TradeDetailsDto UpdateTradeRequest(string email, string identifier, Models.Enums.TradeStatus newStatus)
        {
            throw new NotImplementedException();
        }
    }
}