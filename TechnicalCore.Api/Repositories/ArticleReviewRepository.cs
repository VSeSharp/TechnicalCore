using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalCore.Api.Data;
using TechnicalCore.Api.Data.Entities;

namespace TechnicalCore.Api.Repositories
{
    public class ArticleReviewRepository : IArticleReviewRepository
    {
        private readonly TechnicalCoreDbContext _dbContext;

        public ArticleReviewRepository(TechnicalCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ArticleReview>> GetForArticle(int articleId)
        {
            return await _dbContext.ArticleReviews.Where(pr => pr.ArticleId == articleId).ToListAsync();
        }

        public async Task<ILookup<int, ArticleReview>> GetForArticles(IEnumerable<int> articleIds)
        {
            var reviews = await _dbContext.ArticleReviews.Where(pr => articleIds.Contains(pr.ArticleId)).ToListAsync();
            return reviews.ToLookup(r => r.ArticleId);
        }

        public async Task<ArticleReview> AddReview(ArticleReview review)
        {
            _dbContext.ArticleReviews.Add(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
    }
}
