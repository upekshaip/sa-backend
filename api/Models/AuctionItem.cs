using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace api.Models;


public class AuctionItem
{
    public int AuctionItemId { get; set; }
    public int AuctionId { get; set; }
    // public Auction? Auction { get; set; }

    [StringLength(255)]
    public string ItemName { get; set; } = string.Empty;
    public string ItemDescription { get; set; } = string.Empty;
    public string ItemImage { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string? ItemCategory { get; set; } = string.Empty;

    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}
