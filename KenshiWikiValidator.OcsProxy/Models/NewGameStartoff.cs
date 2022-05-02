using KenshiWikiValidator.OcsProxy.Models.Interfaces;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class NewGameStartoff : ItemBase, IDescriptive
    {
        public NewGameStartoff(string stringId, string name)
            : base(stringId, name)
        {
            this.ForcedRaces = Enumerable.Empty<ItemReference<Race>>();
            this.Squads = Enumerable.Empty<ItemReference<Squad>>();
            this.Characters = Enumerable.Empty<ItemReference<Character>>();
            this.Towns = Enumerable.Empty<ItemReference<Town>>();
            this.FactionRelations = Enumerable.Empty<ItemReference<Faction>>();
            this.Researches = Enumerable.Empty<ItemReference<Research>>();
        }

        public override ItemType Type => ItemType.NewGameStartoff;

        [Value("force start pos")]
        public bool? ForceStartPos { get; set; }

        [Value("money")]
        public int? Money { get; set; }

        [Value("start pos X")]
        public int? StartPosX { get; set; }

        [Value("start pos Z")]
        public int? StartPosZ { get; set; }

        [Value("description")]
        public string? Description { get; set; }

        [Value("difficulty")]
        public string? Difficulty { get; set; }

        [Value("style")]
        public string? Style { get; set; }

        [Reference("force race")]
        public IEnumerable<ItemReference<Race>> ForcedRaces { get; set; }

        [Reference("squad")]
        public IEnumerable<ItemReference<Squad>> Squads { get; set; }

        [Reference("squad")]
        public IEnumerable<ItemReference<Character>> Characters { get; set; }

        [Reference("town")]
        public IEnumerable<ItemReference<Town>> Towns { get; set; }

        [Reference("faction relations")]
        public IEnumerable<ItemReference<Faction>> FactionRelations { get; set; }

        [Reference("research")]
        public IEnumerable<ItemReference<Research>> Researches { get; set; }

    }
}