using MultilingualAPIDemo.Models;

namespace MultilingualAPIDemo.Locale
{
    public static class DataHelper
    {
        public static List<Article> GetArticles()
        {
            return
        [
            new Article { Id = 1, NameKey = "Article1_Name", DescriptionKey = "Article1_Description" },
            new Article { Id = 2, NameKey = "Article2_Name", DescriptionKey = "Article2_Description" },
        ];
        }
    }
}
