using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.DataItemConversion.Builders.Components;
using KenshiWikiValidator.Features.DataItemConversion.Models;
using KenshiWikiValidator.Features.WikiTemplates;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators.Rules
{
    public class ContainsBlueprintTemplateRule : ContainsTemplateRuleBase
    {
        private readonly IItemRepository itemRepository;
        private readonly BlueprintLocationsConverter blueprintLocationsConverter;

        public ContainsBlueprintTemplateRule(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
            this.blueprintLocationsConverter = new BlueprintLocationsConverter(itemRepository);
        }

        protected override WikiTemplate PrepareTemplate(ArticleData data)
        {
            var stringId = data.Get("string id");
            if (string.IsNullOrEmpty(stringId))
            {
                return null!;
            }

            var item = this.itemRepository.GetItemByStringId(stringId);

            if (item is null || item is not IResearchable researchable)
            {
                throw new InvalidOperationException($"{item.Name} is not researchable.");
            }

            var color = item.Type switch
            {
                ItemType.Crossbow => "yellow",
                ItemType.Armour => "green",
                _ => "blue",
            };

            var templateProperties = new SortedList<string, string>();
            if (researchable.UnlockingResearch is null)
            {
                if (!researchable.BlueprintLocations.Any())
                {
                    return null!;
                }

                templateProperties.Add("name", item.Name!);
                templateProperties.Add("color", color);
                templateProperties.Add("description", item.Properties["description"].ToString()!);
                templateProperties.Add("level", "1");
                templateProperties.Add("value", "???");
                templateProperties.Add("prerequisites", string.Empty);
                templateProperties.Add("sell value", "???");
                templateProperties.Add("new items", item.Name!);
                templateProperties.Add("locations", string.Join(", ", researchable.BlueprintLocations.Select(loc => $"[[{loc.Name}]]")));
            }
            else
            {
                var research = this.itemRepository.GetDataItemByStringId(researchable.UnlockingResearch.StringId!);
                int cost = (int)research.Values["money"];

                var requirements = research.GetReferenceItems(this.itemRepository, "requirements");
                var newItems = research.ReferenceCategories.Values
                    .Where(cat => cat.Key.StartsWith("enable"))
                    .SelectMany(cat => cat.Values)
                    .Select(reference => this.itemRepository.GetDataItemByStringId(reference.TargetId));
                var costs = research.GetReferences("cost")
                    .ToDictionary(reference => reference, reference => this.itemRepository.GetDataItemByStringId(reference.TargetId));

                // TODO: Remove these
                var vendors = this.itemRepository.GetReferencingDataItemsFor(research);
                if (vendors.Any(vendor => vendor.Type != ItemType.VendorList && vendor.Type != ItemType.Research))
                {
                    var invalidVendors = vendors.Where(vendor => vendor.Type != ItemType.VendorList && vendor.Type != ItemType.Research);
                    throw new InvalidOperationException($"There are other types than just vendors or research containing '{research.Name}'");
                }

                var blueprintLocations = this.blueprintLocationsConverter.Convert(research, "blueprints");

                if (cost != 0)
                {
                    templateProperties.Add("name", research.Name!);
                    templateProperties.Add("color", color);
                    templateProperties.Add("description", research.Values["description"].ToString()!);
                    templateProperties.Add("level", research.Values["level"].ToString()!);
                    templateProperties.Add("value", string.Format("{0:n0}", cost));
                    templateProperties.Add("prerequisites", string.Join(", ", requirements.Select(req => $"[[{req.Name}]]")));
                    templateProperties.Add("sell value", string.Format("{0:n0}", cost / 4));
                    templateProperties.Add("new items", string.Join(", ", newItems.Select(newItem => $"[[{newItem.Name}]]")));
                    templateProperties.Add("locations", string.Join(", ", blueprintLocations.Select(loc => $"[[{loc.Name}]]")));
                }
            }

            var templateName = "Blueprint";

            return new WikiTemplate(templateName, templateProperties);
        }
    }
}
