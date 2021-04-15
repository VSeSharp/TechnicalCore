using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalCore.Api.Data.Entities;

namespace TechnicalCore.Api.Repositories
{
    public interface IArticleReviewRepository
    {
        Task<IEnumerable<ArticleReview>> GetForArticle(int articleId);
        Task<ILookup<int, ArticleReview>> GetForArticles(IEnumerable<int> articleIds);
        Task<ArticleReview> AddReview(ArticleReview review);
    }
}
