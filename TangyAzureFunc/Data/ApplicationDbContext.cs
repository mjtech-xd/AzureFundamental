using Microsoft.EntityFrameworkCore;
using TangyAzureFunc.Models;

namespace TangyAzureFunc.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<SalesRequest> SalesRequests { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalesRequest>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}