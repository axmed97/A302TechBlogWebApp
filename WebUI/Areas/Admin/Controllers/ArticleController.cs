using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ArticleController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var articles = _context.Articles.ToList();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            var categories = _context.Categories.ToList();
            var tags = _context.Tags.ToList();
            ViewData["tags"] = tags;
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article article, IFormFile file, List<int> tagIds)
        {
            //var path = "uploads/" + Guid.NewGuid() + file.FileName;
            var path = Path.Combine("/uploads/", Guid.NewGuid() + file.FileName);
            using FileStream fileStream = new(_env.WebRootPath + path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return View();
        }
    }
}
