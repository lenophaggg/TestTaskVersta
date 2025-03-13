using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Ordernumber).HasName("orders_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("now()");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
