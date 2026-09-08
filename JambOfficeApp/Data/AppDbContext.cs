using System;
using System.IO;
using JambOfficeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JambOfficeApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<DocumentStatus> DocumentStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(AppContext.BaseDirectory, "jamboffice.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>()
            .HasMany(c => c.Documents)
            .WithOne(d => d.Candidate)
            .HasForeignKey(d => d.CandidateId);
    }
}