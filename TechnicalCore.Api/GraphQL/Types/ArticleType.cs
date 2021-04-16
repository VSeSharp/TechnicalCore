using GraphQL.DataLoader;
using GraphQL.Types;
using TechnicalCore.Api.Data.Entities;
using TechnicalCore.Api.Repositories;

namespace TechnicalCore.Api.GraphQL.Types
{
    public class ArticleType : ObjectGraphType<Article>
    {
        public ArticleType(IArticleReviewRepository reviewRepository, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name).Description("Name of the article");
            Field(t => t.Description).Description("Description of the article");
            Field(t => t.Rating).Description("The (max 5) star customer rating");
            Field(t => t.PostedOn).Description("When the article was posted");

            Field<ListGraphType<ArticleReviewType>>(
               "reviews",
               resolve: context =>
               {
                   var loader =
                       dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, ArticleReview > (
                           "GetReviewsByArticleId", reviewRepository.GetForArticles);
                   return loader.LoadAsync(context.Source.Id);
               });

            Interface<ArticleInterface>();
        }
    }
}
