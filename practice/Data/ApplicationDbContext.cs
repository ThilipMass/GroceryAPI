using System;
using Microsoft.EntityFrameworkCore;
using practice.models;

namespace DBConnection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext):base (dbContext)
        {
            
        }

        public DbSet<User> UserDetails {get;set;}
        public DbSet<Products> ProductDetails {get;set;}
        public DbSet<BookingDetail> BookingDetail {get;set;}
        
    }
}