using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class Payment
{
    public int PaymentId { get; set; }
    public int UserId { get; set; }
    public int AuctionId { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    
    [StringLength(50)]
    public string PaymentMethod { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string Type { get; set; } = string.Empty;
    public bool IsOK { get; set; } = false;
    [StringLength(50)]
    public string PaymentStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
