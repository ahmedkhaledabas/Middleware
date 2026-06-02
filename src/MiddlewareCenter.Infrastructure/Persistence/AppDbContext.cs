using MiddlewareCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareCenter.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core database context for MiddlewareCenter.
/// Handles persistence of internal entities such as request logs.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<RequestLog> RequestLogs => Set<RequestLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RequestLog>(entity =>
        {
            entity.ToTable("RequestLogs");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.TransactionId)
                  .IsRequired()
                  .HasMaxLength(64);

            entity.Property(e => e.Method)
                  .IsRequired()
                  .HasMaxLength(10);

            entity.Property(e => e.Path)
                  .IsRequired()
                  .HasMaxLength(1000);

            entity.Property(e => e.QueryString)
                  .HasMaxLength(2000);

            entity.Property(e => e.RequestBody)
                  .HasColumnType("nvarchar(max)");

            entity.Property(e => e.ResponseBody)
                  .HasColumnType("nvarchar(max)");

            entity.Property(e => e.WindowsUser)
                  .HasMaxLength(256);

            entity.Property(e => e.IpAddress)
                  .HasMaxLength(64);

            entity.Property(e => e.UserAgent)
                  .HasMaxLength(512);

            entity.Property(e => e.ErrorMessage)
                  .HasColumnType("nvarchar(max)");

            entity.Property(e => e.CreatedBy)
                  .HasMaxLength(256);

            entity.Property(e => e.UpdatedBy)
                  .HasMaxLength(256);

            entity.HasIndex(e => e.TransactionId)
                  .HasDatabaseName("IX_RequestLogs_TransactionId");

            entity.HasIndex(e => e.CreatedAt)
                  .HasDatabaseName("IX_RequestLogs_CreatedAt");

            entity.HasIndex(e => e.WindowsUser)
                  .HasDatabaseName("IX_RequestLogs_WindowsUser");
        });
    }
}
