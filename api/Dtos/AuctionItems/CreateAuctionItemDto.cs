using System;

namespace api.Dtos.AuctionItems;

public class CreateAuctionItemDto
{
    public int AuctionId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string ItemDescription { get; set; } = string.Empty;
    public string ItemImage { get; set; } = string.Empty;
    public string? ItemCategory { get; set; } = string.Empty;
}
