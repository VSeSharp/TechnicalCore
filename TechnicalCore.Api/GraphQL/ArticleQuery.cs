using GraphQL.Types;
using TechnicalCore.Api.GraphQL.Types;
using TechnicalCore.Api.Repositories;

namespace TechnicalCore.Api.GraphQL
{
    public class ArticleQuery : ObjectGraphType
    {
        public ArticleQuery(IArticleRepository articleRepository)
        {
            Field<ListGraphType<ArticleType>>(
                "articles",
                resolve: context => articleRepository.GetAll()
            );
        }
    }
}
