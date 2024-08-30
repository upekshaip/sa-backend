using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class Notification
{
    public int Id { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    
    [StringLength(255)]
    public string Status { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
