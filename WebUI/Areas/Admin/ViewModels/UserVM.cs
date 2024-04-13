using WebUI.Models;

namespace WebUI.Areas.Admin.ViewModels
{
    public class UserVM
    {
        public User User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
