namespace TechnicalCore.Web.Models
{
    public class ArticleReviewModel
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
    }
}
