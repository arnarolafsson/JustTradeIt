using System;
using System.Collections.Generic;
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
            List<ItemDto> final = new List<ItemDto>();
            var items = _dbContext.Items.ToList();
            items.ForEach(i =>
            {
                var owner = _dbContext.Users.FirstOrDefault(u => u.Id == i.OwnerId);
                final.Add(new ItemDto
                {
                    Identifier = i.PublicIdentifier,
                    Title = i.Title,
                    ShortDescription = i.ShortDescription,
                    Owner = new UserDto
                    {
                        Identifier = owner.PublicIdentifier,
                        FullName = owner.FullName,
                        Email = owner.Email,
                        ProfileImageUrl = owner.ProfileImageUrl
                    }
                });
            });
            
            if (!ascendingSortOrder) items.OrderByDescending(i => i.Title);
            

            return new Envelope<ItemDto>(pageNumber, pageSize, final);
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            
            var item = _dbContext.Items.FirstOrDefault(i => i.PublicIdentifier == identifier);
            if (item == null) { throw new Exception("item not Found"); }

            var owner = _dbContext.Users.FirstOrDefault(o => o.Id == item.OwnerId);
            var condition = _dbContext.ItemConditions.FirstOrDefault(c => c.Id == item.ItemConditionId).ConditionCode;
            var images = _dbContext.ItemImages.Where(i => i.ItemId == item.Id)
                .Select(img => new ImageDto
                {
                    ImageUrl = img.ImageUrl
                });
            var itemDetails = new ItemDetailsDto
            {
                Identifier = item.PublicIdentifier,
                Title = item.Title,
                Description = item.Description,
                Images = images,
                // TODO active requests
                Condition = condition,
                Owner = new UserDto
                {
                    Identifier = owner.PublicIdentifier,
                    FullName = owner.FullName,
                    Email = owner.Email,
                    ProfileImageUrl = owner.ProfileImageUrl
                }
            };
            return itemDetails;
        }

        public void RemoveItem(string email, string identifier)
        {
            throw new NotImplementedException();
        }
    }
}