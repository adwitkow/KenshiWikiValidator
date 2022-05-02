using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Season : ItemBase
    {
        public Season(string stringId, string name)
            : base(stringId, name)
        {
            this.Weathers = Enumerable.Empty<ItemReference<Weather>>();
        }

        public override ItemType Type => ItemType.Season;

        [Value("weather strength limit max")]
        public float? WeatherStrengthLimitMax { get; set; }

        [Value("weather strength limit min")]
        public float? WeatherStrengthLimitMin { get; set; }

        [Value("sunlight color")]
        public int? SunlightColor { get; set; }

        [Reference("weathers")]
        public IEnumerable<ItemReference<Weather>> Weathers { get; set; }

    }
}