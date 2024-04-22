using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public ArticleController(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Detail(int id)
        {
            var article = _context.Articles
                .Include(x => x.Category)
                .Include(x => x.ArticleTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefault(x => x.Id == id);

            var articles = _context.Articles
                .Where(x => x.IsDeleted == false)
                .ToList();

            var currentArticle = articles.IndexOf(article); // 100 + 1
            var prev = currentArticle <= 0 ? null : articles[currentArticle - 1];
            var next = currentArticle + 1 >= articles.Count ? null : articles[currentArticle + 1];

            var cookie = _contextAccessor.HttpContext.Request.Cookies["Views"];
            string[] findCookie = { "" };

            if (cookie != null)
            {
                findCookie = cookie.Split('-').ToArray();
            }

            if (!findCookie.Contains(article.Id.ToString()))
            {
                Response.Cookies.Append($"Views", $"{cookie}-{article.Id}", 
                    new CookieOptions
                    {
                        Secure = true,
                        HttpOnly = true,
                        Expires = DateTime.Now.AddYears(1)
                    });
                article.ViewCount += 1;
                _context.Articles.Update(article);
                _context.SaveChanges();
            }

            var similar = _context.Articles
                .Include(x => x.Category)
                .Where(x => x.CategoryId == article.CategoryId && x.Id != article.Id)
                .Take(2)
                .ToList();

            DetailVM detailVM = new()
            {
                Article = article,
                SimilarArticles = similar,
                NextArticle = next,
                PrevArticle = prev
            };

            return View(detailVM);
        }
    }
}
