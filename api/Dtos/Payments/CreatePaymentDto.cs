using System;

namespace api.Dtos.Payments;

public class CreatePaymentDto
{   
    public int UserId { get; set; }
    public int AuctionId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;

}
