using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enities;
using Talabat.Repository.Data.Config;

namespace Talabat.Repository.Data
{
    public class StoreContext : DbContext
    {


        /// I dont have to over ride this function because i use the depndancy injection
        ///protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ///{
        ///    Options.UseSqlServer("THe Conniction String for thedata base");
        ///}
 

        // The injection callin on program.cs using builder.Services.AddDbContext
        public StoreContext(DbContextOptions<StoreContext> Options) :base(Options) { 
        
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // To aply all configration that in this Assemply Project
            // Reflection
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Or 
             //modelBuilder.ApplyConfiguration(new ProductConfigrations())
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrand { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }


    }
}
