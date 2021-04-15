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
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleReview> ArticleReviews { get; set; }
    }
}
