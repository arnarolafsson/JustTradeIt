using System.Linq;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllItems([FromQuery] int pageSize = 25, [FromQuery] int pageNumber = 1, [FromQuery] bool ascendingSortOrder = true)
        {
            return Ok(_itemService.GetItems(pageSize, pageNumber, ascendingSortOrder).Items.ToList());
        }

        [HttpPost]
        [Route("")]
        public IActionResult createItem([FromBody] ItemInputModel item)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            return Ok(_itemService.AddNewItem(name, item));
        }
        
    }
}