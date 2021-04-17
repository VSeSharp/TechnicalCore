using GraphQL;
using GraphQL.Types;
using TechnicalCore.Api.GraphQL.Types;
using TechnicalCore.Api.Repositories;

namespace TechnicalCore.Api.GraphQL
{
    public class TechnicalCoreQuery : ObjectGraphType
    {
        public TechnicalCoreQuery(IArticleRepository articleRepository, IArticleReviewRepository reviewRepository)
        {
            Field<ListGraphType<ArticleInterface>>(
                "articles",
                resolve: context => articleRepository.GetAll()
            );

            Field<ArticleType>(
                "article",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> {   Name = "id" }),
                resolve: context =>
                {
                    context.Errors.Add(new ExecutionError("Error occured when resolving article"));
                    var id = context.GetArgument<int>("id");
                    return articleRepository.GetOne(id);
                }
            );

            Field<ListGraphType<ArticleReviewType>>(
                "reviews",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "articleId" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("articleId");
                    return reviewRepository.GetForArticle(id);
                });
        }
    }
}
