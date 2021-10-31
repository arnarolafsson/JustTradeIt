namespace JustTradeIt.Software.API.Models.Entities
{
    public class JwtToken
    {
        public int Id { get; set; }
        public bool blacklisted { get; set; }
    }
}