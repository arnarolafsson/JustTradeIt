using System.Collections.Generic;
using JustTradeIt.Software.API.Models.DTOs;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserDto GetUserInformation(string identifier)
        {
            return _userRepository.GetUserInformation(identifier);
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}