using JustTradeIt.Software.API.Models;

namespace JustTradeIt.Software.API.Services.Interfaces
{
    public interface IItemService
    {
        Envelope<ItemDto> GetItems(int pageSize, int pageNumber, bool ascendingSortOrder);
        ItemDetailsDto GetItemByIdentifier(string identifier);
        string AddNewItem(string email, ItemInputModel item);
        void RemoveItem(string email, string itemIdentifier);
    }
}