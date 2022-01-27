using KenshiWikiValidator.Features.DataItemConversion.Models.Components;

namespace KenshiWikiValidator.Features.DataItemConversion.Models
{
    public interface IResearchable
    {
        ItemReference? UnlockingResearch { get; set; }

        IEnumerable<ItemReference> BlueprintLocations { get; set; }
    }
}
