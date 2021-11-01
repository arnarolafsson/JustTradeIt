using System.Linq;
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

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        public IActionResult RequestTrade([FromBody] TradeInputModel request)
        {
            var email = User.Claims.FirstOrDefault(e => e.Type == "name").Value;
            return Ok(_tradeService.CreateTradeRequest(email, request));
        }
        
    }
}