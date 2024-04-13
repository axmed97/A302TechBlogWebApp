using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Areas.Admin.ViewModels;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> AddRole(string userId)
        {
            var findUser = await _userManager.FindByIdAsync(userId);
            if (findUser == null) return NotFound();

            var userRoles = (await _userManager.GetRolesAsync(findUser)).ToList();
            var roles = _roleManager.Roles.Select(x => x.Name).ToList();

            UserVM userVM = new()
            {
                User = findUser,
                Roles = roles.Except(userRoles),
            };

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string userId, string role)
        {
            var findUser = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.AddToRoleAsync(findUser, role);
            if (result.Succeeded)
                return Redirect("/admin/user/index");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string userId)
        {
            var findUser = await _userManager.FindByIdAsync(userId);
            if (findUser == null) return NotFound();
            return View(findUser);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string userId, string role)
        {
            var findUser = await _userManager.FindByIdAsync(userId);

            await _userManager.RemoveFromRoleAsync(findUser, role);
            return Redirect("/admin/user/index");
        }

    }
}
