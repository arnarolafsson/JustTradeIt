using System;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Implementations;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterInputModel register)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Model is not valid");
            }

            var user = _accountService.CreateUser(register);
            return Ok(_tokenService.GenerateJwtToken(user));
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginInputModel login)
        {
            var user = _accountService.AuthenticateUser(login);
            if (user == null) return Unauthorized();
            return Ok(_tokenService.GenerateJwtToken(user));
        }
    }
}