using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Modles; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;



namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        
        public DbSet<Stock> Stocks { get; set; } 
        public DbSet<Comment> Comments { get; set; }

    }

    
}