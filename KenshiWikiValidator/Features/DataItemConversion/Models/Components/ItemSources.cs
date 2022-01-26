namespace KenshiWikiValidator.Features.DataItemConversion.Models.Components
{
    public class ItemSources
    {
        public ItemSources()
        {
            AlwaysWornBy = new List<ItemReference>();
            PotentiallyWornBy = new List<ItemReference>();
            Shops = new List<ItemReference>();
            Loot = new List<ItemReference>();
        }

        public ICollection<ItemReference> AlwaysWornBy { get; set; }

        public ICollection<ItemReference> PotentiallyWornBy { get; set; }

        public ICollection<ItemReference> Shops { get; set; }

        public ICollection<ItemReference> Loot { get; set; }
    }
}
