using System;
using System.Linq;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Implementations;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginInputModel login)
        {
            var user = _accountService.AuthenticateUser(login);
            if (user == null) return Unauthorized();
            return Ok(_tokenService.GenerateJwtToken(user));
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "tokenId").Value, out var tokenId);
            _accountService.Logout(tokenId);
            return NoContent();
        }

        [HttpGet]
        [Route("profile")]
        public IActionResult GetProfileInfo()
        {
            var name = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            return Ok(_accountService.GetProfileInformation(name));
        }

        [HttpPut]
        [Route("profile")]
        public IActionResult UpdateProfile([FromForm] ProfileInputModel profile)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            _accountService.UpdateProfile(name, profile);
            return Ok("hmmm");
        }
    }
}