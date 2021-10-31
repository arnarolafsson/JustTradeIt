﻿using System;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ModifiedByDate { get; set; }
        public string ModifiedBy { get; set; }
        public string TradeStatus { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
    }
}