using System;

namespace api.Dtos.Bids;

public class BidDto
{
    public int BidId { get; set; }
    public int AuctionId { get; set; }
    public string BidderName { get; set; } = string.Empty;
    public int BidderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal BidAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
