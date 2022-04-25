using System.Text.Json;

namespace KenshiWikiValidator.WikiCategories.Locations
{
    public class ZoneDataProvider
    {
        private const string FilePath = "ZoneData.json";

        private ILookup<string, string> zones;

        public IEnumerable<string> GetZones(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException(nameof(location));
            }

            return this.zones[location];
        }

        public async Task Load()
        {
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using var fs = File.OpenRead(FilePath);
            var items = await JsonSerializer.DeserializeAsync<IEnumerable<LocationZoneItem>>(fs, options);

            if (items == null)
            {
                throw new InvalidDataException("Zone data is empty.");
            }

            this.zones = items.ToLookup(data => data.Name, data => data.Zone);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "This is correct for records, IMO.")]
        private record LocationZoneItem(string Name, string Zone);
    }
}
