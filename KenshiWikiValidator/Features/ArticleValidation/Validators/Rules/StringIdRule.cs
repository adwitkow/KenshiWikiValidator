using KenshiWikiValidator.Features.DataItemConversion;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators.Rules
{
    public class StringIdRule : IValidationRule
    {
        private readonly IItemRepository itemRepository;
        private readonly WikiTitleCache wikiTitleCache;

        public StringIdRule(IItemRepository itemRepository, WikiTitleCache wikiTitleCache)
        {
            this.itemRepository = itemRepository;
            this.wikiTitleCache = wikiTitleCache;
        }

        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();

            var validTemplates = new[] { "Weapon", "Armour", "Traders" };

            if (!validTemplates.Any(template => content.Contains("{{" + template)))
            {
                return result;
            }

            using var reader = new StringReader(content);

            // TODO: Cleanup
            string? line;
            var matchingItems = this.itemRepository
                .GetDataItems()
                .Where(item => title.ToLower().Trim().Equals(item.Name!.ToLower().Trim()))
                .ToList();
            var stringIdFound = false;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("fcs_name"))
                {
                    var pairs = line.Split("|");
                    var elements = pairs.SelectMany(pair => pair.Split("=")).ToArray();
                    for (var i = 0; i < elements.Length; i++)
                    {
                        var element = elements[i].Trim();
                        if (element.Contains("fcs_name"))
                        {
                            var fcsName = elements[i + 1].Trim();
                            matchingItems = this.itemRepository
                                .GetDataItems()
                                .Where(item => item.Name.ToLower() == fcsName.ToLower())
                                .ToList();
                        }
                    }
                }

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

                            if (matchingItem is null && matchingItems.Any())
                            {
                                result.AddIssue($"String id '{stringId}' is incorrect in the article. Should be corrected to one of the following: [{string.Join(", ", matchingItems.Select(item => item.StringId))}]");
                            }
                            else
                            {
                                data.Add("string id", stringId); // TODO: this will probably need to have some exceptions
                                this.wikiTitleCache.AddTitle(stringId, title);
                            }

                            stringIdFound = true;
                        }
                    }
                }
            }

            if (!stringIdFound)
            {
                result.AddIssue($"No string id! Most likely string id: [{string.Join(", ", matchingItems.Select(item => $"string id = {item.StringId}|"))}]");
            }

            return result;
        }
    }
}
