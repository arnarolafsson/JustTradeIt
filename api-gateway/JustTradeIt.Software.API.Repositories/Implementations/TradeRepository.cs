using System;
using System.Collections.Generic;
using System.Linq;
using JustTradeIt.Software.API.Models.DTOs;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

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
            var nextTradeId = _dbContext.TradeItems.Select(i => i.TradeId).DefaultIfEmpty().Max() + 1;
            // Add items to TradeItem
            trade.ItemsInTrade.ToList().ForEach(i =>
            {
                var item = _dbContext.Items.FirstOrDefault(j => j.PublicIdentifier == i.Identifier);
                if (item == null) { throw new Exception("Item does not exist"); }
                if (item.OwnerId != sender.Id && item.OwnerId != receiver.Id)
                {
                    throw new Exception("You can not trade this item");
                }
                var tradeItem = new TradeItem
                {
                    TradeId = nextTradeId,
                    UserId = sender.Id,
                    ItemId = item.Id
                        
                };
                _dbContext.TradeItems.Add(tradeItem);
                _dbContext.SaveChanges();

            });
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
            _dbContext.Trades.Add(tradeRequest);
            _dbContext.SaveChanges();
            // TODO: RABBITMQ
            return tradeRequest.PublicIdentifier;
        }

        public TradeDetailsDto GetTradeByIdentifier(string identifier)
        {
            List<ItemDto> RecItems = new List<ItemDto>();
            List<ItemDto> SendItems = new List<ItemDto>();
            var trade = _dbContext.Trades.FirstOrDefault(i => i.PublicIdentifier == identifier);
            if (trade == null) throw new Exception("Trade does not exist!!");
            var receiver = _dbContext.Users.FirstOrDefault(i => i.Id == trade.ReceiverId);
            var sender = _dbContext.Users.FirstOrDefault(i => i.Id == trade.SenderId);

            var senderItemIds = _dbContext.TradeItems.Where(i => i.TradeId == trade.Id).ToList();
            senderItemIds.ForEach(i =>
            {
                var item = _dbContext.Items.FirstOrDefault(j => j.Id == i.ItemId);
                if (item.OwnerId == sender.Id)
                {
                    SendItems.Add(new ItemDto
                    {
                        Identifier = item.PublicIdentifier,
                        Title = item.Title,
                        ShortDescription = item.ShortDescription,
                        Owner = new UserDto
                        {
                            Identifier = sender.PublicIdentifier,
                            FullName = sender.FullName,
                            Email = sender.Email,
                            ProfileImageUrl = sender.ProfileImageUrl
                        }
                        
                    });
                }
                else
                {
                    RecItems.Add(new ItemDto
                    {
                        Identifier = item.PublicIdentifier,
                        Title = item.Title,
                        ShortDescription = item.ShortDescription,
                        Owner = new UserDto
                        {
                            Identifier = receiver.PublicIdentifier,
                            FullName = receiver.FullName,
                            Email = receiver.Email,
                            ProfileImageUrl = receiver.ProfileImageUrl
                        }
                    });
                }
                
                
            });
            return new TradeDetailsDto
            {
                Identifier = trade.PublicIdentifier,
                ReceivingItems = RecItems,
                OfferingItems = SendItems,
                Receiver = new UserDto
                {
                    Identifier = receiver.PublicIdentifier,
                    FullName = receiver.FullName,
                    Email = receiver.Email,
                    ProfileImageUrl = receiver.ProfileImageUrl
                },
                Sender = new UserDto
                {
                    Identifier = sender.PublicIdentifier,
                    FullName = sender.FullName,
                    Email = sender.Email,
                    ProfileImageUrl = sender.ProfileImageUrl
                },
                IssuedDate = trade.IssueDate,
                ModifiedDate = trade.ModifiedByDate,
                ModifiedBy = trade.ModifiedBy,
                Status = trade.TradeStatus
            };
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            List<TradeDto> Out = new List<TradeDto>();
            var user = _dbContext.Users.FirstOrDefault(i => i.Email == email);
            var trades = _dbContext.Trades.Where(i => i.ReceiverId == user.Id || i.SenderId == user.Id).ToList();
            trades.ForEach(i =>
            {
                if (i.TradeStatus == TradeStatus.Accepted)
                {
                    Out.Add(new TradeDto
                    {
                        Identifier = i.PublicIdentifier,
                        IssuedDate = i.IssueDate,
                        ModifiedDate = i.ModifiedByDate,
                        ModifiedBy = i.ModifiedBy
                    });
                }
            });
            return Out;
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public void UpdateTradeRequest(string identifier, string email, TradeStatus newStatus)
        {
            var trade = _dbContext.Trades.FirstOrDefault(i => i.PublicIdentifier == identifier);
            if (trade == null) { throw new Exception("Trade does not exist!"); }
            var user = _dbContext.Users.FirstOrDefault(i => i.Email == email);
            if (trade.TradeStatus != TradeStatus.Pending)
            {
                throw new Exception("Trade has already been finalized!");
            }

            if (user.Id == trade.SenderId && newStatus != TradeStatus.Cancelled)
            {
                throw new Exception("You can only cancel your order");
            }

            if (user.Id == trade.ReceiverId && newStatus == TradeStatus.Cancelled)
            {
                throw new Exception("You can only Accept or Decline the trade!");
            }
            trade.ModifiedByDate = DateTime.Now;
            trade.ModifiedBy = user.FullName;
            trade.TradeStatus = newStatus;
            _dbContext.SaveChanges();

            

            // TODO rabbitmq
            

        }
    }
}