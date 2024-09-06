using System;

namespace api.Dtos.Bids;

public class CreateBidDto
{
    public int AuctionId { get; set; }
    public int BidderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal BidAmount { get; set; }
}