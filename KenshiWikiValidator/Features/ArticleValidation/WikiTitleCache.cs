using KenshiWikiValidator.Features.DataItemConversion.Models;

namespace KenshiWikiValidator.Features.ArticleValidation
{
    public class WikiTitleCache
    {
        private readonly Dictionary<string, string> data;

        public WikiTitleCache()
        {
            this.data = new Dictionary<string, string>();
        }

        public bool HasArticle(IItem item)
        {
            return this.data.ContainsKey(item.StringId);
        }

        public string? GetTitle(IItem item)
        {
            var success = this.data.TryGetValue(item.StringId, out var title);
            if (success)
            {
                return title;
            }
            else
            {
                return item.Name;
            }
        }

        public void AddTitle(string stringId, string title)
        {
            var exists = this.data.TryGetValue(stringId, out var existingValue);

            if (exists && title.Equals(existingValue))
            {
                return;
            }

            this.data.Add(stringId, title);
        }
    }
}