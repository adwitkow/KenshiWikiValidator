using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.DataItemConversion.Models
{
    public interface IDataItem
    {
        public ItemType Type { get; }

        public IDictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }
    }
}
