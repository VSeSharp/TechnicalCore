using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalCore.Api.Data;
using TechnicalCore.Api.Data.Entities;

namespace TechnicalCore.Api.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly TechnicalCoreDbContext _dbContext;

        public ArticleRepository(TechnicalCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Article>> GetAll()
        {
            return _dbContext.Articles.ToListAsync();
        }
    }
}
