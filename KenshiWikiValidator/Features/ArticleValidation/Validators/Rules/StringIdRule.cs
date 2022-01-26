using KenshiWikiValidator.Features.DataItemConversion;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators.Rules
{
    public class StringIdRule : IValidationRule
    {
        private readonly IItemRepository itemRepository;

        public StringIdRule(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public RuleResult Execute(string title, string content)
        {
            var result = new RuleResult();

            if (!content.Contains("{{Weapon") && !content.Contains("{{Armour"))
            {
                return result;
            }

            using var reader = new StringReader(content);

            string? line;
            var matchingItems = this.itemRepository
                .GetItems()
                .Where(item => title.ToLower().Trim().Equals(item.Name!.ToLower().Trim()))
                .ToList();
            var stringIdFound = false;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("string id"))
                {
                    var pairs = line.Split("|");
                    var elements = pairs.SelectMany(pair => pair.Split("=")).ToArray();
                    for (var i = 0; i < elements.Length; i++)
                    {
                        var element = elements[i].Trim();
                        if (element.Contains("string id"))
                        {
                            var stringId = elements[i + 1].Trim();

                            var matchingItem = matchingItems.FirstOrDefault(item => item.StringId == stringId);

                            if (matchingItem is null)
                            {
                                result.AddIssue($"String id '{stringId}' is incorrect in the article. Should be corrected to one of the following: [{string.Join(", ", matchingItems.Select(item => item.StringId))}]");
                            }

                            stringIdFound = true;
                        }
                    }
                }
            }

            if (!stringIdFound)
            {
                result.AddIssue($"No string id! Most likely string id: [{string.Join(", ", matchingItems.Select(item => item.StringId))}]");
            }

            return result;
        }
    }
}
