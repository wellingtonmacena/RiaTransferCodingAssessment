using Microsoft.EntityFrameworkCore;
using RESTServer.Models;

namespace RESTServer.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}