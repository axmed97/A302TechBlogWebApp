using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin, Super Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole identityRole)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var checkRole = await _roleManager.FindByNameAsync(identityRole.Name);

            if (checkRole != null)
            {
                //ModelState.AddModelError("error", "Role name exist!");
                ViewData["Error"] = "Role name exist!";
                return View();
            }

            await _roleManager.CreateAsync(identityRole);
            return RedirectToAction("Index");
        }
    }
}
