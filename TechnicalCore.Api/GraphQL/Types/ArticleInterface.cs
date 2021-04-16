using GraphQL.Types;
using TechnicalCore.Api.Data.Entities;

namespace TechnicalCore.Api.GraphQL.Types
{
    public class ArticleInterface : InterfaceGraphType<Article>
    {
        public ArticleInterface()
        {
            Field(t => t.Id);
            Field(t => t.Name).Description("Name of the article");
            Field(t => t.Description).Description("Description of the article");
            Field(t => t.Rating).Description("The (max 5) star customer rating");
            Field(t => t.PostedOn).Description("When the article was posted");
        }
    }
}
