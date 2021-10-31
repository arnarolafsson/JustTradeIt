using JustTradeIt.Software.API.Models;

namespace JustTradeIt.Software.API.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Envelope<ItemDto> GetAllItems(int pageSize, int pageNumber, bool ascendingSortOrder);
        ItemDetailsDto GetItemByIdentifier(string identifier);
        string AddNewItem(string email, ItemInputModel item);
        void RemoveItem(string email, string identifier);
    }
}