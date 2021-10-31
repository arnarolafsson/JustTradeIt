﻿using System;
using JustTradeIt.Software.API.Models.Enums;

namespace JustTradeIt.Software.API.Models.DTOs
{
    public class TradeDto
    {
        public string Identifier { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public TradeStatus Status { get; set; }
    }
}