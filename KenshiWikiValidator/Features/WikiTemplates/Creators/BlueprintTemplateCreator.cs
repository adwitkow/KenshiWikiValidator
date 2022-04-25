using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.OcsProxy;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.WikiTemplates.Creators;

public class BlueprintTemplateCreator : ITemplateCreator
{
    private readonly IItemRepository itemRepository;
    private readonly ArticleData data;

    public BlueprintTemplateCreator(IItemRepository itemRepository, ArticleData data)
    {
        this.itemRepository = itemRepository;
        this.data = data;
    }

    public WikiTemplate Generate()
    {
        var stringId = this.data.StringIds.SingleOrDefault();
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

        var templateProperties = new SortedList<string, string?>();
        if (researchable.UnlockingResearch is null)
        {
            if (!researchable.BlueprintSquads.Any())
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

            if (cost != 0)
            {
                templateProperties.Add("name", research.Name!);
                templateProperties.Add("color", color);
                templateProperties.Add("description", research.Values["description"].ToString()!);
                templateProperties.Add("level", research.Values["level"].ToString()!);
                templateProperties.Add("value", string.Format("{0:n0}", cost));
                templateProperties.Add("prerequisites", string.Join(", ", requirements.Select(req => $"[[{req.Name} (Tech)]]")));
                templateProperties.Add("sell value", string.Format("{0:n0}", cost / 4));
                templateProperties.Add("new items", string.Join(", ", newItems.Select(newItem => $"[[{newItem.Name}]]")));
            }
            else
            {
                return null!;
            }
        }

        var templateName = "Blueprint";

        return new WikiTemplate(templateName, templateProperties);
    }
}
