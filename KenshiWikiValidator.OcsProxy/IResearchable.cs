using KenshiWikiValidator.OcsProxy.SharedComponents;

namespace KenshiWikiValidator.OcsProxy
{
    public interface IResearchable
    {
        ItemReference? UnlockingResearch { get; set; }

        IEnumerable<ItemReference> BlueprintSquads { get; set; }
    }
}
