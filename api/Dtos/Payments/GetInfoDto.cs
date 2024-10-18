using System;

namespace api.Dtos.Payments;

public class GetInfoDto
{
    public int UserId { get; set; }
    public int AuctionId { get; set; }
    public string Type { get; set; } = string.Empty;
}
