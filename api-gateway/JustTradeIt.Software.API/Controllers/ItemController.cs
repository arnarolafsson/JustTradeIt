using System.Linq;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private string _email;
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

        [HttpGet]
        [Route("{identifier}", Name = "GetItem")]
        public IActionResult GetItem(string identifier)
        {
            return Ok(_itemService.GetItemByIdentifier(identifier));
        }

        [HttpPost]
        [Route("")]
        public IActionResult createItem([FromBody] ItemInputModel item)
        {
            _email = User.Claims.FirstOrDefault(e => e.Type == "name").Value;
            return Ok(_itemService.AddNewItem(_email, item));
        }

        [HttpDelete]
        [Route("{identifier}", Name = "RemoveItem")]
        public IActionResult RemoveItem(string identifier)
        {
            _email = User.Claims.FirstOrDefault(e => e.Type == "name").Value;
            _itemService.RemoveItem(_email, identifier);
            return NoContent();
        }
    }
}