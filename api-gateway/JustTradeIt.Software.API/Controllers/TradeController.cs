using System.Linq;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/trades")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;
        private string _email;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
            
        }
        [HttpPost]
        [Route("")]
        public IActionResult RequestTrade([FromBody] TradeInputModel request)
        {
            _email = User.Claims.FirstOrDefault(e => e.Type == "name").Value;
            return Ok(_tradeService.CreateTradeRequest(_email, request));
        }

        [HttpGet]
        public IActionResult GetTrades([FromQuery] bool onlyCompleted = true, [FromQuery] bool onlyIncludeActive = false)
        {
            _email = User.Claims.FirstOrDefault(e => e.Type == "name").Value;
            if(!onlyCompleted)
            {
                return Ok(_tradeService.GetTradeRequests(_email, onlyIncludeActive));
            }
            return Ok(_tradeService.GetTrades(_email));
        }

        [HttpGet]
        [Route("{identifier}", Name = "GetTradeByIdentifier")]
        public IActionResult GetTradeByIdentifier(string identifier)
        {
            return Ok(_tradeService.GetTradeByIdentifer(identifier));
        }

        [HttpPatch]
        [Route("{identifier}", Name = "UpdateTradeRequest")]
        public IActionResult UpdateTradeRequest(string identifier, [FromBody]string Status)
        {
            _email = User.Claims.FirstOrDefault(e => e.Type == "name").Value;
            _tradeService.UpdateTradeRequest(identifier, _email, Status);
            return NoContent();
        }
        
    }
}