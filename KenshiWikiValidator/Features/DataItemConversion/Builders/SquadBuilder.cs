using KenshiWikiValidator.Features.DataItemConversion.Models;
using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders
{
    internal class SquadBuilder : IItemBuilder<Squad>
    {
        private readonly IItemRepository itemRepository;

        public SquadBuilder(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public Squad Build(DataItem baseItem)
        {
            var aiPackages = baseItem.GetReferenceItems(this.itemRepository, "AI packages");

            var isShop = aiPackages.Any(package => package
                .GetReferenceItems(this.itemRepository, "Leader AI Goals")
                .Where(reference => "Shopkeeper".Equals(reference.Name))
                .Any());

            var towns = this.itemRepository.GetReferencingDataItemsFor(baseItem)
                .Where(item => item.Type == ItemType.Town);
            var townReferences = new List<ItemReference>();

            foreach (var town in towns)
            {
                if (!town.Name.ToLower().Contains("override"))
                {
                    townReferences.Add(new ItemReference(town.StringId, town.Name));
                    continue;
                }

                var parents = this.itemRepository.GetReferencingDataItemsFor(town)
                    .Where(item => !item.Name.ToLower().Contains("override"));
                var townFactionReference = town.GetReferences("faction").SingleOrDefault();

                string townFaction;
                if (townFactionReference is null)
                {
                    townFaction = "Destroyed";
                }
                else
                {
                    var townFactionId = townFactionReference.Key;
                    townFaction = this.itemRepository.GetDataItemByStringId(townFactionId).Name;
                }

                foreach (var parent in parents)
                {
                    var parentFactionId = parent.GetReferences("faction").Single().Key;
                    var parentFaction = this.itemRepository.GetDataItemByStringId(parentFactionId).Name;

                    if (parentFaction != townFaction)
                    {
                        townReferences.Add(new ItemReference(town.StringId, $"{parent.Name}, {townFaction}"));
                    }
                    else
                    {
                        townReferences.Add(new ItemReference(town.StringId, parent.Name));
                    }
                }
            }

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
    }
}
