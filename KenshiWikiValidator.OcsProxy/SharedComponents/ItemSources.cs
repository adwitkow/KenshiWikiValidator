namespace KenshiWikiValidator.OcsProxy.SharedComponents
{
    public class ItemSources
    {
        public ItemSources()
        {
            this.AlwaysWornBy = new List<ItemReference>();
            this.PotentiallyWornBy = new List<ItemReference>();
            this.Shops = new List<ItemReference>();
            this.Loot = new List<ItemReference>();
        }

        public ICollection<ItemReference> AlwaysWornBy { get; set; }

        public ICollection<ItemReference> PotentiallyWornBy { get; set; }

        public ICollection<ItemReference> Shops { get; set; }

        public ICollection<ItemReference> Loot { get; set; }
    }
}
