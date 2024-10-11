using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace api.Models;

public class Auction
{
    public int AuctionId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AuctionImage { get; set; } = string.Empty;
    public string AuctionCategory { get; set; } = string.Empty;
    public int? SellerId { get; set; }
    // public User? Seller { get; set; }
    public int? WinnerId { get; set; }
    // public User? Winner { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal StartingBid { get; set; }
    

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? WinningBid { get; set; }


    [StringLength(100)]
    public string? Status { get; set; } = string.Empty;
    
    
    [StringLength(100)]
    public string? IsLive { get; set; } = string.Empty;

    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public List<AuctionItem> AuctionItems { get; set; } = new List<AuctionItem>();
    public List<Bid> Bids { get; set; } = new List<Bid>();
}
