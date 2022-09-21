using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Developer> Developers { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration):base(dbContextOptions)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(a =>
            {
                a.ToTable("Languages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.LanguageId).HasColumnName("LanguageId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasOne(p => p.Language);
            });

            modelBuilder.Entity<Developer>(p =>
            {
                p.ToTable("Developers");
                p.HasMany(p => p.GitHubProfiles);
            });

            modelBuilder.Entity<User>(p =>
            {
                p.ToTable("Users").HasKey(k => k.Id);
                p.Property(p => p.Id).HasColumnName("Id");
                p.Property(p => p.FirstName).HasColumnName("FirstName");
                p.Property(p => p.LastName).HasColumnName("LastName");
                p.Property(p => p.Email).HasColumnName("Email");
                p.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                p.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                p.Property(p => p.Status).HasColumnName("Status").HasDefaultValue(true);
                p.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");
                p.HasMany(p => p.UserOperationClaims);
                p.HasMany(p => p.RefreshTokens);
            });

            modelBuilder.Entity<OperationClaim>(p =>
            {
                p.ToTable("OperationClaims").HasKey(k => k.Id);
                p.Property(p => p.Id).HasColumnName("Id");
                p.Property(p => p.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(p =>
            {
                p.ToTable("UserOperationClaims").HasKey(k => k.Id);
                p.Property(p => p.Id).HasColumnName("Id");
                p.Property(p => p.UserId).HasColumnName("UserId");
                p.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
                p.HasOne(p => p.OperationClaim);
                p.HasOne(p => p.User);
            });

            modelBuilder.Entity<GitHubProfile>(p =>
            {
                p.ToTable("GitHubProfiles").HasKey(k => k.Id);
                p.Property(p => p.Id).HasColumnName("Id");
                p.Property(p => p.DeveloperId).HasColumnName("DeveloperId");
                p.Property(p => p.ProfileUrl).HasColumnName("ProfileUrl");
                p.HasOne(p => p.Developer);
            });




            Language[] languageEntitySeeds = { new(1, "C#"), new(2, "Java") };
            modelBuilder.Entity<Language>().HasData(languageEntitySeeds);

            Technology[] technologyEntitySeeds = { new(1, 2, "Spring"), new(2,1, "ASP.NET") };
            modelBuilder.Entity<Technology>().HasData(technologyEntitySeeds);
        }
    }
}
