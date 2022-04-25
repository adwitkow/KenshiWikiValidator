using KenshiWikiValidator.OcsProxy.Builder;
using KenshiWikiValidator.OcsProxy.SharedComponents;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.SquadComponents
{
    internal class SquadBuilder : ItemBuilderBase<Squad>
    {
        private readonly IItemRepository itemRepository;

        public SquadBuilder(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public override Squad Build(DataItem baseItem)
        {
            var isShop = this.IsShop(baseItem);
            var townReferences = this.GetLocations(baseItem);

            var squad = new Squad(baseItem.Values, baseItem.StringId, baseItem.Name)
            {
                IsShop = isShop,
                Locations = townReferences,
            };

            return squad;
        }

        private IEnumerable<ItemReference> GetLocations(DataItem baseItem)
        {
            var towns = this.itemRepository.GetReferencingDataItemsFor(baseItem)
                            .Where(item => item.Type == ItemType.Town);
            var locations = new List<ItemReference>();

            foreach (var town in towns)
            {
                var townReferences = this.GetTownReferences(town);
                locations.AddRange(townReferences);
            }

            return locations;
        }

        private IEnumerable<ItemReference> GetTownReferences(DataItem town)
        {
            var results = new List<ItemReference>();
            if (!town.Name.ToLower().Contains("override"))
            {
                results.Add(new ItemReference(town.StringId, town.Name));
                return results;
            }

            var townFaction = this.GetTownFaction(town);

            var parents = this.itemRepository.GetReferencingDataItemsFor(town)
                .Where(item => !item.Name.ToLower().Contains("override"));
            foreach (var parent in parents)
            {
                var parentFactionId = parent.GetReferences("faction").Single().Key;
                var parentFaction = this.itemRepository.GetDataItemByStringId(parentFactionId).Name;

                if (parentFaction != townFaction)
                {
                    results.Add(new ItemReference(town.StringId, $"{parent.Name}/{townFaction}"));
                }
                else
                {
                    results.Add(new ItemReference(town.StringId, parent.Name));
                }
            }

            return results;
        }

        private string GetTownFaction(DataItem town)
        {
            var townFactionReference = town.GetReferences("faction").SingleOrDefault();

            if (townFactionReference is null)
            {
                return "Destroyed";
            }
            else
            {
                var townFactionId = townFactionReference.Key;
                return this.itemRepository.GetDataItemByStringId(townFactionId).Name;
            }
        }

        private bool IsShop(DataItem baseItem)
        {
            var aiPackages = baseItem.GetReferenceItems(this.itemRepository, "AI packages");

            var isShop = aiPackages.Any(package => package
                .GetReferenceItems(this.itemRepository, "Leader AI Goals")
                .Where(reference => "Shopkeeper".Equals(reference.Name))
                .Any());
            return isShop;
        }
    }
}
