using System;
using api.Models;

namespace api.Dtos.Auctions;
public class AuctionDto
{
    public int AuctionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AuctionImage { get; set; } = string.Empty;
    public string AuctionCategory { get; set; } = string.Empty;
    public int? SellerId { get; set; }
    // public User? WinnerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal StartingBid { get; set; }
    // public decimal? WinningBid { get; set; }
    public string? Status { get; set; } = string.Empty;
    public string? IsLive { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    // public List<AuctionItem> AuctionItems { get; set; } = new List<AuctionItem>();
    // public List<Bid> Bids { get; set; } = new List<Bid>();
}
