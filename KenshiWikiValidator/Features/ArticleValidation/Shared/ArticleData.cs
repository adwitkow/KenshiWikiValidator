using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.ArticleValidation.Shared
{
    public class ArticleData
    {
        public ArticleData()
        {
            this.WikiTemplates = new List<WikiTemplate>();
        }

        public string? StringId { get; set; }

        public IEnumerable<WikiTemplate> WikiTemplates { get; set; }
    }
}
