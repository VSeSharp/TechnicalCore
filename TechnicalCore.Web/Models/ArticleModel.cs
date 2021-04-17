using System;
using System.Collections.Generic;

namespace TechnicalCore.Web.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset PostedOn { get; set; }
        public List<ArticleReviewModel> Reviews { get; set; }
    }
}
