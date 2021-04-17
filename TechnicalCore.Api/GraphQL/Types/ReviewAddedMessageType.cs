using GraphQL.Types;
using TechnicalCore.Api.GraphQL.Messaging;

namespace TechnicalCore.Api.GraphQL.Types
{
    public class ReviewAddedMessageType : ObjectGraphType<ReviewAddedMessage>
    {
        public ReviewAddedMessageType()
        {
            Field(t => t.ArticleId);
            Field(t => t.Title);
        }
    }
}
