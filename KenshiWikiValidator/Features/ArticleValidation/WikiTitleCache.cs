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

        public bool HasArticle(IDataItem item)
        {
            return this.HasArticle(item.StringId);
        }

        public bool HasArticle(string stringId)
        {
            return this.data.ContainsKey(stringId);
        }

        public string GetTitle(IDataItem item)
        {
            return this.GetTitle(item.StringId, item.Name);
        }

        public string GetTitle(string stringId, string itemName)
        {
            var success = this.data.TryGetValue(stringId, out var title);
            if (success)
            {
                return title!;
            }
            else
            {
                return itemName;
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