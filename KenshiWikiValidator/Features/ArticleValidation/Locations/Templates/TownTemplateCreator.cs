using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.WikiTemplates;
using KenshiWikiValidator.Features.WikiTemplates.Creators;
using KenshiWikiValidator.OcsProxy;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.ArticleValidation.Locations.Templates
{
    internal class TownTemplateCreator : ITemplateCreator
    {
        private const string WikiTemplateName = "Town";

        private readonly IItemRepository itemRepository;
        private readonly ZoneDataProvider zoneDataProvider;
        private readonly WikiTitleCache wikiTitles;
        private readonly ArticleData data;
        private readonly string[] townTypes;

        public TownTemplateCreator(IItemRepository itemRepository, ZoneDataProvider zoneDataProvider, WikiTitleCache wikiTitles, ArticleData data)
        {
            this.itemRepository = itemRepository;
            this.zoneDataProvider = zoneDataProvider;
            this.wikiTitles = wikiTitles;
            this.data = data;

            this.townTypes = new[]
            {
                "Nest",
                "Outpost",
                "Town",
                "Village",
                "Ruins",
                "Slave camp",
                "Military",
                "Prison",
                "NEST MARKER",
                "POI",
                "NULL",
            };
        }

        public WikiTemplate? Generate()
        {
            var stringIds = this.data.StringIds;
            if (!stringIds.Any())
            {
                if (string.IsNullOrEmpty(this.data.PotentialStringId))
                {
                    return null;
                }
                else
                {
                    stringIds = new[] { this.data.PotentialStringId };
                }
            }

            var items = stringIds.Select(stringId => this.itemRepository.GetDataItemByStringId(stringId));
            var articleTitle = this.wikiTitles.GetTitle(stringIds.First(), items.First().Name);
            var baseArticleTitle = articleTitle.Split('/').First();

            var existingTemplate = this.data.WikiTemplates
                .SingleOrDefault(template => template.Name.ToLower().Equals("town"));

            var factions = items
                .SelectMany(item => item.GetReferenceItems(this.itemRepository, "faction")
                    .Select(faction => $"[[{faction.Name}]]"))
                .Distinct();

            var zones = this.ExtractZones(items, baseArticleTitle);

            var regions = string.Join(", ", zones
                .Select(region => $"[[{region}]]")
                .Distinct());
            var fcsNames = string.Join(", ", items
                .Where(item => !item.Name.Equals(articleTitle))
                .Select(item => item.Name)
                .Distinct());

            var properties = new SortedList<string, string?>
            {
                { "string id", string.Join(", ", stringIds) },
                { "fcs_name", fcsNames },
                { "type", this.townTypes[items.Min(item => (int)item.Values["type"])] },
                { "biome", regions },
                { "image1", this.GetExistingParameter(existingTemplate, "image1") },
                { "caption1", this.GetExistingParameter(existingTemplate, "caption1") },
                { "map", this.GetExistingParameter(existingTemplate, "map") },
                { "caption2", this.GetExistingParameter(existingTemplate, "caption2") },
                { "factions", string.Join(", ", factions) },
            };

            if (!fcsNames.All(name => name.Equals(articleTitle)))
            {
                properties.Add("title1", baseArticleTitle);
            }

            return new WikiTemplate(WikiTemplateName, properties);
        }

        private IEnumerable<string> ExtractZones(IEnumerable<OpenConstructionSet.Data.Models.DataItem> items, string baseArticleTitle)
        {
            var zones = this.zoneDataProvider.GetZones(baseArticleTitle);
            if (zones.Any())
            {
                return zones;
            }

            zones = this.zoneDataProvider.GetZones(items.Single().Name);
            if (zones.Any())
            {
                return zones;
            }

            var baseTowns = this.FindBaseItems(items);

            if (!baseTowns.Any())
            {
                var joinedItems = string.Join(", ", items.Select(item => $"'{item.Name}'"));
                throw new InvalidOperationException($"Cannot find any zones for base article '{baseArticleTitle}' or items '{joinedItems}'");
            }

            zones = this.zoneDataProvider.GetZones(baseTowns.Single().Name);

            return zones;
        }

        private IEnumerable<OpenConstructionSet.Data.Models.DataItem> FindBaseItems(IEnumerable<OpenConstructionSet.Data.Models.DataItem> items)
        {
            var baseItems = items;
            var previousBaseItems = items;
            while (baseItems is not null && baseItems.Any())
            {
                previousBaseItems = baseItems;
                baseItems = baseItems
                    .SelectMany(item => this.itemRepository.GetReferencingDataItemsFor(item))
                    .Where(item => item.Type == ItemType.Town)
                    .Distinct()
                    .ToList();
            }

            return previousBaseItems;
        }

        private string? GetExistingParameter(WikiTemplate? existingTemplate, string parameter)
        {
            if (existingTemplate is null)
            {
                return null;
            }

            existingTemplate.Parameters.TryGetValue(parameter, out var value);

            return value;
        }
    }
}