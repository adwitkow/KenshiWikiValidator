namespace KenshiWikiValidator.OcsProxy
{
    public readonly struct ItemReference<T> where T : IItem
    {
        public ItemReference(T item, int value0, int value1, int value2)
        {
            this.Item = item;
            this.Value0 = value0;
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public T Item { get; }

        public int Value0 { get; }

        public int Value1 { get; }

        public int Value2 { get; }
    }
}
