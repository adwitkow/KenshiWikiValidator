using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.ArticleValidation.Shared
{
    public class ArticleData
    {
        public ArticleData()
        {
            this.WikiTemplates = new List<WikiTemplate>();
            this.StringIds = new List<string>();
            this.Categories = new List<string>();
            this.PotentialStringId = string.Empty;
        }

        public ICollection<string> StringIds { get; set; }

        public ICollection<string> Categories { get; set; }

        public IEnumerable<WikiTemplate> WikiTemplates { get; set; }

        public string PotentialStringId { get; set; }
    }
}
