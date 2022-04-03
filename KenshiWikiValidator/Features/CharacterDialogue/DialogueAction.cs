﻿using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.CharacterDialogue
{
    public class DialogueAction : IItem
    {
        public ItemType Type => ItemType.DialogAction;

        public Dictionary<string, object>? Properties { get; set; }

        public string StringId { get; set; }

        public string Name { get; set; }
    }
}
