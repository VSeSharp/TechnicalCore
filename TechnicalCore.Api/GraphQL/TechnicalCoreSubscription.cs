using GraphQL.Resolvers;
using GraphQL.Types;
using TechnicalCore.Api.GraphQL.Messaging;
using TechnicalCore.Api.GraphQL.Types;

namespace TechnicalCore.Api.GraphQL
{
    public class TechnicalCoreSubscription : ObjectGraphType
    {
        public TechnicalCoreSubscription(ReviewMessageService messageService)
        {
            Name = "Subscription";
            Description = "The subscription type, represents all updates can be pushed to the client in real time over web sockets.";
            AddField(new EventStreamFieldType
            {
                Name = "reviewAdded",
                Description = "Subscribe to review created events.",
                Type = typeof(ReviewAddedMessageType),
                Resolver = new FuncFieldResolver<ReviewAddedMessage>(c => 
                    c.Source as ReviewAddedMessage),
                Subscriber = new EventStreamResolver<ReviewAddedMessage>(c => 
                    messageService.GetMessages())
            });
        }
    }
}
