﻿using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.WikiTemplates;
using OpenConstructionSet.Data.Models;

namespace KenshiWikiValidator.Features.ArticleValidation.Shared.Rules
{
    public class StringIdRule : IValidationRule
    {
        private static readonly string[] ValidTemplateNames = { "Weapon", "Armour", "Traders", "Town" };

        private readonly IItemRepository itemRepository;
        private readonly WikiTitleCache wikiTitleCache;
        private readonly bool shouldCheckFcsName;

        public StringIdRule(IItemRepository itemRepository, WikiTitleCache wikiTitleCache, bool shouldCheckFcsName = false)
        {
            this.itemRepository = itemRepository;
            this.wikiTitleCache = wikiTitleCache;
            this.shouldCheckFcsName = shouldCheckFcsName;
        }

        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();

            var validTemplates = data.WikiTemplates.Where(template => ValidTemplateNames.Any(valid => template.Name.Equals(valid)));
            if (!validTemplates.Any())
            {
                return result;
            }

            var matchingItems = this.GetMatchingItems(title);
            var fcsNameValue = this.SelectSingleParameter(validTemplates, "fcs_name");

            if (string.IsNullOrEmpty(fcsNameValue))
            {
                if (this.shouldCheckFcsName)
                {
                    result.AddIssue("FCS name is missing!");
                }
            }
            else
            {
                matchingItems.Clear();
                var fcsNames = fcsNameValue.Split(',').Select(name => name.Trim());

                foreach (var fcsName in fcsNames)
                {
                    matchingItems.AddRange(this.GetMatchingItems(fcsName));
                }
            }

            var stringIdValue = this.SelectSingleParameter(validTemplates, "string id");
            if (!string.IsNullOrEmpty(stringIdValue))
            {
                var stringIds = stringIdValue.Split(',')
                    .Select(id => id.Trim());
                foreach (var stringId in stringIds)
                {
                    var matchingItem = matchingItems.FirstOrDefault(item => item.StringId == stringId);

                    if (matchingItem is null)
                    {
                        if (matchingItems.Any())
                        {
                            result.AddIssue($"String id '{stringId}' is incorrect in the article. Should be corrected to one of the following: [{string.Join(", ", matchingItems.Select(item => item.StringId))}]");
                        }
                        else
                        {
                            result.AddIssue($"String id '{stringId}' could not be found in the game files.");
                        }
                    }
                    else
                    {
                        data.StringIds.Add(stringId);
                        this.wikiTitleCache.AddTitle(stringId, title);
                    }
                }
            }
            else
            {
                result.AddIssue($"No string id! Most likely string id: [{string.Join(", ", matchingItems.Select(item => $"string id = {item.StringId}|"))}]");
            }

            return result;
        }

        private string? SelectSingleParameter(IEnumerable<WikiTemplate> validTemplates, string parameterName)
        {
            return validTemplates.Where(template => template.Parameters.ContainsKey(parameterName))
                .Select(template => template.Parameters[parameterName])
                .SingleOrDefault();
        }

        private List<DataItem> GetMatchingItems(string name)
        {
            return this.itemRepository
                .GetDataItems()
                .Where(item => name.ToLower().Trim().Equals(item.Name.ToLower().Trim()))
                .ToList();
        }
    }
}
