using GraphQL.Types;
using TechnicalCore.Api.Data.Entities;

namespace TechnicalCore.Api.GraphQL.Types
{
    public class ArticleReviewType : ObjectGraphType<ArticleReview>
    {
        public ArticleReviewType()
        {
            Field(t => t.Id);
            Field(t => t.Title);
            Field(t => t.Review);
        }
    }
}
