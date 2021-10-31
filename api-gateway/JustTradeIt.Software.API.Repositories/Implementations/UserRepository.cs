using System;
using System.Linq;
using JustTradeIt.Software.API.Models.DTOs;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Helpers;
using JustTradeIt.Software.API.Repositories.Interfaces;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly TradeDbContext _dbContext;
        private readonly ITokenRepository _tokenRepository;
        private string _salt = "00209b47-08d7-475d-a0fb-20abf0872ba0";

        public UserRepository(TradeDbContext dbContext, ITokenRepository tokenRepository)
        {
            _dbContext = dbContext;
            _tokenRepository = tokenRepository;
        }
        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Email == loginInputModel.Email &&
                u.HashedPassword == HashHelper.HashPassword(loginInputModel.Password, _salt));
            if (user == null) return null;

            var token = _tokenRepository.CreateNewToken();


            return new UserDto
            {
                Identifier = user.PublicIdentifier,
                Email = user.Email,
                FullName = user.FullName,
                TokenId = token.Id
            };
        }

        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            var checkEmail = _dbContext.Users.FirstOrDefault(u =>
                u.Email == inputModel.Email);
            if (checkEmail != null) { return null; }
            var token = _tokenRepository.CreateNewToken();
            var hashedPassword = HashHelper.HashPassword(inputModel.Password, _salt);
            var newUser = new User
            {
                PublicIdentifier = Guid.NewGuid().ToString(),
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                HashedPassword = hashedPassword
            };
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return new UserDto
            {
                Identifier = newUser.PublicIdentifier,
                Email = newUser.Email,
                FullName = newUser.FullName,
                TokenId = token.Id
            };
        }

        public UserDto GetProfileInformation(string email)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserInformation(string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile(string email, string profileImageUrl, ProfileInputModel profile)
        {
            throw new NotImplementedException();
        }
    }
}