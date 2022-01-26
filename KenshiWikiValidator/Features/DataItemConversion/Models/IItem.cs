﻿using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Models
{
    public interface IItem
    {
        public ItemType Type { get; }

        public Dictionary<string, object>? Properties { get; set; }

        public string? StringId { get; set; }

        public string? Name { get; set; }
    }
}