using Microsoft.EntityFrameworkCore;
using SupportTickestAPI.Models;

namespace SupportTickestAPI.Data;

public class SupportContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<TicketsModel> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=Support;Username=postgres;Password=admin");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).IsRequired();
            entity.Property(u => u.Email).IsRequired();
        });

        modelBuilder.Entity<TicketsModel>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Title).IsRequired();
            entity.Property(t => t.Description).IsRequired();
        
            // Salva o Enum como STRING no banco 
            entity.Property(t => t.StatusTicket)
                .HasConversion<string>();
        
            entity.HasOne(t => t.UserModel)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    
        base.OnModelCreating(modelBuilder);
    }
}