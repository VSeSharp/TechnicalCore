using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalCore.Api.Data.Entities;

namespace TechnicalCore.Api.Repositories
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAll();
    }
}
