using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TechnicalCore.Web.Clients;
using TechnicalCore.Web.Models;

namespace TechnicalCore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ArticleHttpClient _httpClient;
        private readonly ArticleGraphClient _articleGraphClient;

        public HomeController(
            ILogger<HomeController> logger, 
            ArticleHttpClient httpClient, 
            ArticleGraphClient articleGraphClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _articleGraphClient = articleGraphClient;
        }

        public async Task<IActionResult> Index()
        {
            var responseModel = await _httpClient.GetArticles();
            responseModel.ThrowErrors();
            return View(responseModel.Data.Articles);
        }

        public async Task<IActionResult> ArticleDetail(int articleId)
        {
            _articleGraphClient.SubscribeToUpdates();
            var article = await _articleGraphClient.GetArticle(articleId);
            return View(article);
        }

        public IActionResult AddReview(int articleId)
        {
            return View(new ArticleReviewModel { ArticleId = articleId });
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ArticleReviewModel reviewModel)
        {
            await _articleGraphClient.AddReview(reviewModel);
            return RedirectToAction("ArticleDetail", new { articleId = reviewModel.ArticleId });
        }
    }
}
