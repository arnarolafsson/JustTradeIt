namespace JustTradeIt.Software.API.Services.Interfaces
{
    public interface ITokenService
    {
         string GenerateJwtToken(UserDto user);
    }
}