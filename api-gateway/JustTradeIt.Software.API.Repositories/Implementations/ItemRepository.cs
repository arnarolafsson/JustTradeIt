using System;
using System.Linq;
using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.DTOs;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly TradeDbContext _dbContext;

        public ItemRepository(TradeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string AddNewItem(string email, ItemInputModel item)
        {
            var nextId = _dbContext.Items.Select(r => r.Id).Max() + 1;

            var conditionid = _dbContext.ItemConditions.FirstOrDefault(
                c => c.ConditionCode == item.ConditionCode).Id;
            var userId = _dbContext.Users.FirstOrDefault(u => u.Email == email).Id;
            var newItem = new Item
            {
                Id = nextId,
                PublicIdentifier = Guid.NewGuid().ToString(),
                Title = item.Title,
                Description = item.Description,
                ShortDescription = item.ShortDescription,
                ItemConditionId = conditionid,
                OwnerId = userId
            };
            item.ItemImages.ToList().ForEach(i =>
            {
                var newItemImage = new ItemImage
                {
                    ImageUrl = i,
                    ItemId = newItem.Id
                };
                _dbContext.ItemImages.Add(newItemImage);
                _dbContext.SaveChanges();
            });
            
            _dbContext.Items.Add(newItem);
            _dbContext.SaveChanges();
            return newItem.PublicIdentifier;
        }

        public Envelope<ItemDto> GetAllItems(int pageSize, int pageNumber, bool ascendingSortOrder)
        {
            var itemss = _dbContext.Items.ToList();
            itemss.ForEach(i =>
            {
                
            });
            var items = _dbContext.Items.Select(i => new ItemDto
            {
                Identifier = i.PublicIdentifier,
                Title = i.Title,
                ShortDescription = i.ShortDescription,
                
            }).ToList();
            if (!ascendingSortOrder) items.OrderByDescending(i => i.Title);
            

            return new Envelope<ItemDto>(pageNumber, pageSize, items);
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string email, string identifier)
        {
            throw new NotImplementedException();
        }
    }
}