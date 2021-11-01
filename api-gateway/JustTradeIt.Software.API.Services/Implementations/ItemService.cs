using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.DTOs;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public string AddNewItem(string email, ItemInputModel item)
        {
            return _itemRepository.AddNewItem(email, item);
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            throw new System.NotImplementedException();
        }

        public Envelope<ItemDto> GetItems(int pageSize, int pageNumber, bool ascendingSortOrder)
        {
            return _itemRepository.GetAllItems(pageSize, pageNumber, ascendingSortOrder);
        }

        public void RemoveItem(string email, string itemIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}