using GraphQL;
using GraphQL.Types;
using TechnicalCore.Api.GraphQL.Types;
using TechnicalCore.Api.Repositories;

namespace TechnicalCore.Api.GraphQL
{
    public class TechnicalCoreQuery : ObjectGraphType
    {
        public TechnicalCoreQuery(IArticleRepository articleRepository)
        {
            Field<ListGraphType<ArticleType>>(
                "articles",
                resolve: context => articleRepository.GetAll()
            );

            Field<ArticleType>(
                "article",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                    {   Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return articleRepository.GetOne(id);
                }
            );
        }
    }
}
