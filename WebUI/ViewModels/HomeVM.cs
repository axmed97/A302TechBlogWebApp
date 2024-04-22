using WebUI.Models;

namespace WebUI.ViewModels
{
    public class HomeVM
    {
        public List<Article> FeaturedArticles { get; set; }
        public List<Article> PopularArticles { get; set; }
        public List<Article> Articles { get; set; }
    }
}
