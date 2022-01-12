using OpenConstructionSet.Models;

namespace KenshiDataSnooper.Models
{
    public class Character : IItem
    {
        public ItemType Type => ItemType.Character;

        public Dictionary<string, object>? Properties { get; set; }

        public string? StringId { get; set; }

        public string? Name { get; set; }
    }
}
