namespace KenshiWikiValidator.OcsProxy
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ReferenceAttribute : Attribute
    {
        public ReferenceAttribute(string category)
        {
            this.Category = category;
        }

        public string Category { get; }
    }
}