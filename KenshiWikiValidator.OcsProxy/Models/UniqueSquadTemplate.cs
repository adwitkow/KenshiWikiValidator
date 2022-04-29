using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class UniqueSquadTemplate : ItemBase
    {
        public UniqueSquadTemplate(string stringId, string name)
            : base(stringId, name)
        {
            this.FallbackAiPackage = Enumerable.Empty<ItemReference<AiPackage>>();
            this.Leader = Enumerable.Empty<ItemReference<Character>>();
            this.Missions = Enumerable.Empty<ItemReference<DiplomaticAssaults>>();
            this.Squad = Enumerable.Empty<ItemReference<Character>>();
        }

        public override ItemType Type => ItemType.UniqueSquadTemplate;

        [Value("building ruined")]
        public bool? BuildingRuined { get; set; }

        [Value("dont multiply")]
        public bool? DontMultiply { get; set; }

        [Value("malnourished")]
        public bool? Malnourished { get; set; }

        [Value("patrol approaches towns")]
        public bool? PatrolApproachesTowns { get; set; }

        [Value("persistent")]
        public bool? Persistent { get; set; }

        [Value("public beds")]
        public bool? PublicBeds { get; set; }

        [Value("public day")]
        public bool? PublicDay { get; set; }

        [Value("public night")]
        public bool? PublicNight { get; set; }

        [Value("roaming military")]
        public bool? RoamingMilitary { get; set; }

        [Value("animal age random")]
        public float? AnimalAgeRandom { get; set; }

        [Value("blood smell mult")]
        public float? BloodSmellMult { get; set; }

        [Value("bed usage cost")]
        public int? BedUsageCost { get; set; }

        [Value("building designation")]
        public int? BuildingDesignation { get; set; }

        [Value("crossbow levels")]
        public int? CrossbowLevels { get; set; }

        [Value("force speed")]
        public int? ForceSpeed { get; set; }

        [Value("initial door state")]
        public int? InitialDoorState { get; set; }

        [Value("num random chars")]
        public int? NumRandomChars { get; set; }

        [Value("num random chars max")]
        public int? NumRandomCharsMax { get; set; }

        [Value("replacement time")]
        public int? ReplacementTime { get; set; }

        [Value("robotics levels")]
        public int? RoboticsLevels { get; set; }

        [Value("building name")]
        public string? BuildingName { get; set; }

        [Value("layout exterior")]
        public string? LayoutExterior { get; set; }

        [Value("layout interior")]
        public string? LayoutInterior { get; set; }

        [Reference("fallback AI package")]
        public IEnumerable<ItemReference<AiPackage>> FallbackAiPackage { get; set; }

        [Reference("leader")]
        public IEnumerable<ItemReference<Character>> Leader { get; set; }

        [Reference("missions")]
        public IEnumerable<ItemReference<DiplomaticAssaults>> Missions { get; set; }

        [Reference("squad")]
        public IEnumerable<ItemReference<Character>> Squad { get; set; }

    }
}