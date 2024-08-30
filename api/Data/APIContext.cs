using System;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class APIContext : DbContext
{
    public APIContext(DbContextOptions<APIContext> options)
                : base(options)
            {
            }

    public DbSet<User> Users { get; set; }
}
