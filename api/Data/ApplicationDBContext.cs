using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Modles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;



namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        
        public DbSet<Stock> Stocks { get; set; } 
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Portfolio>(x=>x.HasKey(p=> new {p.AppUserId,p.StockId}));
            modelBuilder.Entity<Portfolio>()
            .HasOne(u=>u.AppUser)
            .WithMany(u=>u.Portfolios)
            .HasForeignKey(p=>p.AppUserId);

            modelBuilder.Entity<Portfolio>()
            .HasOne(u=>u.Stock)
            .WithMany(u=>u.Portfolios)
            .HasForeignKey(p=>p.StockId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole {Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole {Name = "User", NormalizedName = "USER"},
                new IdentityRole {Name = "Moderator", NormalizedName = "MODERATOR"}
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }


    
}