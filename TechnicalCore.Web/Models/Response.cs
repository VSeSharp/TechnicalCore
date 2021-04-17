using System.Collections.Generic;
using System.Linq;

namespace TechnicalCore.Web.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public List<ErrorModel> Errors { get; set; }

        public void ThrowErrors()
        {
            if (Errors != null && Errors.Any())
                throw new GraphQlException(
                    $"Message: {Errors[0].Message} Code: {Errors[0].Code}");
        }
    }

    public class ArticlesContainer
    {
        public List<ArticleModel> Articles { get; set; }
    }
}
