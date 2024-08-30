using System;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AuctionContext : DbContext
{
    public AuctionContext(DbContextOptions<AuctionContext> options)
                : base(options)
            {
            }

    public DbSet<User> Users { get; set; }
}
