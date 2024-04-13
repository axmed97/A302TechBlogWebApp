using Microsoft.AspNetCore.Mvc;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]  
        public IActionResult Create(Category category)
        {
            var findCategory = _context.Categories.FirstOrDefault(x => x.CategoryName == category.CategoryName);
            if(findCategory != null)
            {
                ViewBag.Error = "Category name is already exist!";
                return View(findCategory);
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Redirect("/admin/category");
        }

        public IActionResult Edit(int id)
        {
            var findCategory = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (findCategory == null)
                return NotFound();

            return View(findCategory);
        }

        [HttpPost] 
        public IActionResult Edit(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return Redirect("/admin/category/index");
        }

    }
}
