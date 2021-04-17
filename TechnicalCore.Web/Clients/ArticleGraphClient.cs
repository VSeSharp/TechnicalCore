using GraphQL;
using GraphQL.Client.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TechnicalCore.Web.Models;

namespace TechnicalCore.Web.Clients
{
    public class ArticleGraphClient
    {
        private readonly GraphQLHttpClient _client;

        public ArticleGraphClient(GraphQLHttpClient client)
        {
            _client = client;
        }

        public async Task<ArticleModel> GetArticle(int id)
        {
            var request = new GraphQLRequest
            {
                Query = @" 
                query articleQuery($articleId: ID!)
                { article(id: $articleId) 
                    { id name rating description stock 
                      reviews { title review }
                    }
                }",
                Variables = new { articleId = id }
            };
            GraphQLResponse<ArticleModel> response = await _client.SendQueryAsync<ArticleModel>(request);
            return response.Data;
        }

        public async Task<ArticleReviewModel> AddReview(ArticleReviewModel review)
        {
            var request = new GraphQLRequest
            {
                Query = @" 
                mutation($review: reviewInput!)
                {
                    createReview(review: $review)
                    {
                        id
                    }
                }",
                Variables = new { review }
            };
            GraphQLResponse<ArticleReviewModel> response = await _client.SendQueryAsync<ArticleReviewModel>(request);
            return response.Data;
        }

        public void SubscribeToUpdates()
        {

            var request = new GraphQLRequest
            {
                Query = @" 
                subscription 
                { reviewAdded 
                    { title articleId } 
                }"
            };

            IObservable<GraphQLResponse<object>> subscriptionStream = _client.CreateSubscriptionStream<object>(request, (e) =>
            {
               Debug.WriteLine(e.Message); // I receive the message here
            });

            var subscription = subscriptionStream.Subscribe((response) =>
            {
                if (response?.Data != null)
                {
                    Debug.WriteLine(response?.Data);
                }
            }, (error) =>
            {
                Debug.WriteLine(string.Format("Exception when subscribe: {0}", error.Message));
            }, () =>
            {
                Debug.WriteLine("Subscription Completed");
            });
        }
    }
}
