using JambOffuceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JambOffuceApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<DocumentStatus> DocumentStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=jamboffice.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>()
            .HasMany(c => c.Documents)
            .WithOne(d => d.Candidate)
            .HasForeignKey(d => d.CandidateId);
    }
}