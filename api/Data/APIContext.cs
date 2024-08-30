using System;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class APIContext : DbContext
{
    public APIContext(DbContextOptions<APIContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<AuctionItem> AuctionItems { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Payment> Payments { get; set; }
}
