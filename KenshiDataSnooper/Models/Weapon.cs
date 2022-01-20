﻿using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiDataSnooper.Models
{
    public class Weapon : IItem
    {
        public ItemType Type => ItemType.Weapon;

        public Dictionary<string, object>? Properties { get; set; }

        public string? StringId { get; set; }

        public string? Name { get; set; }
    }
}