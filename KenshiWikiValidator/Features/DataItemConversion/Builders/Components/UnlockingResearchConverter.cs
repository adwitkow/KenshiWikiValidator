using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders.Components
{
    internal class UnlockingResearchConverter
    {
        private readonly ItemRepository itemRepository;

        public UnlockingResearchConverter(ItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public ItemReference? Convert(DataItem baseItem)
        {
            var researchItems = this.itemRepository.GetReferencingDataItemsFor(baseItem)
                .Where(item => item.Type == ItemType.Research);

            ItemReference? result = null;
            if (researchItems.Any())
            {
                var research = researchItems.Single();
                return new ItemReference(research.StringId, research.Name);
            }

            return result;
        }
    }
}
