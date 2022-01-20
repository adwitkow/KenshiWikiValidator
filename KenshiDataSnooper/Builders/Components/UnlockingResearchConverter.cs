﻿using KenshiDataSnooper.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiDataSnooper.Builders.Components
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
                return new ItemReference()
                {
                    Name = research.Name,
                    StringId = research.StringId,
                };
            }

            return result;
        }
    }
}
