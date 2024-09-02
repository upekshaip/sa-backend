using System;

namespace api.Dtos.Users;

public class CreateUserDto
{
    public string FirstName { get; set; } = string.Empty; 
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int Mobile { get; set; }
    public string Password { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    // public int UserId { get; set; }
    // public string Token { get; set; } = string.Empty;
    // public DateTime CreatedAt { get; private set; } = DateTime.Now;
    // public DateTime UpdatedAt { get; set; } = DateTime.Now;
    // public List<Bid> Bids { get; set; } = new List<Bid>();
    // public List<Auction> Auctions { get; set; } = new List<Auction>();
}
