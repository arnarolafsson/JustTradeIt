using System.Collections.Generic;

namespace JustTradeIt.Software.API.Models.DTOs
{
    public class ItemDetailsDto
    {
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<ImageDto> Images { get; set; }
        public int NumberOfActiveRequests { get; set; }
        public string Condition { get; set; }
        public UserDto Owner { get; set; }
    }
}