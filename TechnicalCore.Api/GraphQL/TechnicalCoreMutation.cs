using GraphQL;
using GraphQL.Types;
using TechnicalCore.Api.Data.Entities;
using TechnicalCore.Api.GraphQL.Messaging;
using TechnicalCore.Api.GraphQL.Types;
using TechnicalCore.Api.Repositories;

namespace TechnicalCore.Api.GraphQL
{
    public class TechnicalCoreMutation : ObjectGraphType
    {
        //PROBLEM
        public TechnicalCoreMutation(/*ArticleReviewRepository reviewRepository, ReviewMessageService messageService*/)
        {
            //FieldAsync<ArticleReviewType>(
            //    "createReview",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<ArticleReviewInputType>> { Name = "review" }),

            //    resolve: async context =>
            //    {
            //        var review = context.GetArgument<ArticleReview>("review");
            //        await reviewRepository.AddReview(review);
            //        messageService.AddReviewAddedMessage(review);
            //        return review;
            //    });
        }
    }
}
