using Microsoft.EntityFrameworkCore;
using TechnicalCore.Api.Data.Entities;

namespace TechnicalCore.Api.Data
{
    public class TechnicalCoreDbContext : DbContext
    {
        public TechnicalCoreDbContext(DbContextOptions<TechnicalCoreDbContext> options) 
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //config primary key(Product & Category)
            modelBuilder.Entity<Article>().HasKey(s => s.Id);
        }
        public DbSet<Article> Articles { get; set; }
    }
}
