using WebUI.Models;

namespace WebUI.ViewModels
{
    public class DetailVM
    {
        public Article Article { get; set; }
        public Article PrevArticle { get; set; }
        public Article NextArticle { get; set; }
        public List<Article> SimilarArticles { get; set; }

    }
}
