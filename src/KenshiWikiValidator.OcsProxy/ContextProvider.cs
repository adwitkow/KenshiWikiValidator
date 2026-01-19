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

using GameFinder.RegistryUtils;
using GameFinder.StoreHandlers.Steam;
using NexusMods.Paths;
using OpenConstructionSet;
using OpenConstructionSet.Installations;
using OpenConstructionSet.Installations.Locators;
using OpenConstructionSet.Installations.Settings;
using OpenConstructionSet.Mods;
using OpenConstructionSet.Mods.Context;

namespace KenshiWikiValidator.OcsProxy
{
    public class ContextProvider
    {
        public IModContext GetDataMiningContext()
        {
            // I don't think this will be used more than once
            // in any context, but just in case:
            // TODO: Cache
            var locatorHandler = new SteamHandler(FileSystem.Shared, WindowsRegistry.Shared);
            var installationFactory = new InstallationFactory(new SaveFolderHelper(new SettingsHelper()));
            var locator = new SteamLocator(locatorHandler, installationFactory);
            var installationService = new InstallationService([locator]);
            var installation = installationService.LocateAll().First(); // TODO: Handle errors

            var options = new ModContextOptions(
                name: Guid.NewGuid().ToString(),
                installation: installation,
                loadGameFiles: ModLoadType.Base,
                loadEnabledMods: ModLoadType.None,
                throwIfMissing: false);

            var builder = new ContextBuilder();
            return builder.BuildAsync(options).Result;
        }
    }
}
