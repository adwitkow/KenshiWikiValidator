using KenshiWikiValidator.Features.DataItemConversion.Models;
using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders
{
    internal class SquadBuilder : ItemBuilderBase<Squad>
    {
        private readonly IItemRepository itemRepository;

        public SquadBuilder(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public override Squad Build(IItem baseItem)
        {
            var isShop = this.IsShop(baseItem);
            var townReferences = this.GetLocations(baseItem);

            var squad = new Squad()
            {
                StringId = baseItem.StringId,
                Name = baseItem.Name,
                Properties = baseItem.Values,
                IsShop = isShop,
                Locations = townReferences,
            };

            return squad;
        }

        private IEnumerable<ItemReference> GetLocations(IItem baseItem)
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

        private IEnumerable<ItemReference> GetTownReferences(IItem town)
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
                var parentFactionId = parent.GetReferences("faction").Single().TargetId;
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

        private string GetTownFaction(IItem town)
        {
            var townFactionReference = town.GetReferences("faction").SingleOrDefault();

            if (townFactionReference is null)
            {
                return "Destroyed";
            }
            else
            {
                var townFactionId = townFactionReference.TargetId;
                return this.itemRepository.GetDataItemByStringId(townFactionId).Name;
            }
        }

        private bool IsShop(IItem baseItem)
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
