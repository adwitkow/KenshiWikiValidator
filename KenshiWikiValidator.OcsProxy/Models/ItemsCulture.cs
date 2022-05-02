using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class ItemsCulture : ItemBase
    {
        public ItemsCulture(string stringId, string name)
            : base(stringId, name)
        {
            this.ForbiddenItems = Enumerable.Empty<ItemReference<Item>>();
            this.IllegalBuildings = Enumerable.Empty<ItemReference<Building>>();
            this.IllegalGoods = Enumerable.Empty<ItemReference<Item>>();
            this.TradePrices = Enumerable.Empty<ItemReference<Item>>();
        }

        public override ItemType Type => ItemType.ItemsCulture;

        [Reference("forbidden items")]
        public IEnumerable<ItemReference<Item>> ForbiddenItems { get; set; }

        [Reference("illegal buildings")]
        public IEnumerable<ItemReference<Building>> IllegalBuildings { get; set; }

        [Reference("illegal goods")]
        public IEnumerable<ItemReference<Item>> IllegalGoods { get; set; }

        [Reference("trade prices")]
        public IEnumerable<ItemReference<Item>> TradePrices { get; set; }

    }
}