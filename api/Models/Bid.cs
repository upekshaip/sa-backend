using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace api.Models;

public class Bid
{
    public int BidId { get; set; }
    public int AuctionId { get; set; }
    public required Auction Auction { get; set; }
    public int BidderId { get; set; }
    public required User Bidder { get; set; }
    

    [StringLength(50)]
    public string Status { get; set; } = string.Empty;
    
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal BidAmount { get; set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
