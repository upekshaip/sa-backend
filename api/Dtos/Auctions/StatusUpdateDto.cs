using System;

namespace api.Dtos.Auctions;

public class StatusUpdateDto
{
    public int Id { get; set; }
    public int AuctionId { get; set; }
    public string IsLive { get; set; } = string.Empty;
}
