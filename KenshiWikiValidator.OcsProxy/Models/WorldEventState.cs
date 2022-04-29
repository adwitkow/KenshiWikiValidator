using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class WorldEventState : ItemBase
    {
        public WorldEventState(string stringId, string name)
            : base(stringId, name)
        {
            this.NpcIs = Enumerable.Empty<ItemReference<Character>>();
            this.NpcIsNot = Enumerable.Empty<ItemReference<Character>>();
            this.PlayerAlly = Enumerable.Empty<ItemReference<Faction>>();
            this.PlayerEnemy = Enumerable.Empty<ItemReference<Faction>>();
        }

        public override ItemType Type => ItemType.WorldEventState;

        [Value("player involvement")]
        public bool? PlayerInvolvement { get; set; }

        [Value("notes")]
        public string? Notes { get; set; }

        [Reference("NPC is")]
        public IEnumerable<ItemReference<Character>> NpcIs { get; set; }

        [Reference("NPC is NOT")]
        public IEnumerable<ItemReference<Character>> NpcIsNot { get; set; }

        [Reference("player ally")]
        public IEnumerable<ItemReference<Faction>> PlayerAlly { get; set; }

        [Reference("player enemy")]
        public IEnumerable<ItemReference<Faction>> PlayerEnemy { get; set; }

    }
}