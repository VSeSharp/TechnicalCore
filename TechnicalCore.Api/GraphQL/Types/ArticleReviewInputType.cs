using GraphQL.Types;

namespace TechnicalCore.Api.GraphQL.Types
{
    public class ArticleReviewInputType : InputObjectGraphType
    {
        public ArticleReviewInputType()
        {
            Name = "reviewInput";
            Field<IdGraphType>("id");
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<StringGraphType>("review");
            Field<NonNullGraphType<IntGraphType>>("articleId");
        }
    }
}
