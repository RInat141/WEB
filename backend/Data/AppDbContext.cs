using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Site> Sites => Set<Site>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Привязка к именам таблиц из ЛР9 (snake_case)
        modelBuilder.Entity<Ticket>().ToTable("tickets");
        modelBuilder.Entity<Site>().ToTable("sites");
        modelBuilder.Entity<User>().ToTable("users");

        // Настройка связей и ограничений
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Site)
            .WithMany()
            .HasForeignKey(t => t.SiteId);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedByUserId);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AssigneeUser)
            .WithMany()
            .HasForeignKey(t => t.AssigneeUserId)
            .IsRequired(false); // nullable
    }
}