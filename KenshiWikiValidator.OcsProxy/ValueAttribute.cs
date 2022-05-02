namespace KenshiWikiValidator.OcsProxy
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ValueAttribute : Attribute
    {
        public ValueAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}