namespace TechnicalCore.Api.GraphQL.Messaging
{
    public class ReviewAddedMessage
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
    }
}
