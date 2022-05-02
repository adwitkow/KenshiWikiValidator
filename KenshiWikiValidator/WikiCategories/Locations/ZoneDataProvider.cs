// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
// Copyright (C) 2021  Adam Witkowski <https://github.com/adwitkow/>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Text.Json;

namespace KenshiWikiValidator.WikiCategories.Locations
{
    public class ZoneDataProvider
    {
        private const string FilePath = "ZoneData.json";

        private ILookup<string, string>? zones;

        public IEnumerable<string> GetZones(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException(nameof(location));
            }

            if (this.zones is null)
            {
                throw new InvalidOperationException("Zones collection is null");
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
        private sealed record LocationZoneItem(string Name, string Zone);
    }
}
