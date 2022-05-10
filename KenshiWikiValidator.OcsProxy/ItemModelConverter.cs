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

using KenshiWikiValidator.OcsProxy.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy
{
    public class ItemModelConverter
    {
        private readonly Dictionary<ItemType, Func<DataItem, IItem>> conversionMap;
        private readonly ItemMapper mapper;

        public ItemModelConverter(IItemRepository itemRepository)
        {
            this.conversionMap = this.CreateConversionMap();
            this.mapper = new ItemMapper(itemRepository);
        }

        public IItem Convert(DataItem item)
        {
            var result = this.conversionMap[item.Type].Invoke(item);

            return result;
        }

        public IEnumerable<(DataItem baseItem, IItem result)> Convert(IEnumerable<DataItem> contextItems)
        {
            return contextItems.Select(baseItem => (baseItem, this.Convert(baseItem)));
        }

        public IItem MapProperties((DataItem Base, IItem Result) convertedPair)
        {
            return this.mapper.Map(convertedPair.Base, convertedPair.Result);
        }

        private Dictionary<ItemType, Func<DataItem, IItem>> CreateConversionMap()
        {
            return new Dictionary<ItemType, Func<DataItem, IItem>>()
            {
                { ItemType.LocationalDamage, (item) => new LocationalDamage(item.StringId, item.Name) },
                { ItemType.Item, (item) => new Models.Item(item.StringId, item.Name) },
                { ItemType.Building, (item) => new Building(item.StringId, item.Name) },
                { ItemType.DialogueLine, (item) => new DialogueLine(item.StringId, item.Name) },
                { ItemType.DialogAction, (item) => new DialogAction(item.StringId, item.Name) },
                { ItemType.Dialogue, (item) => new Dialogue(item.StringId, item.Name) },
                { ItemType.VendorList, (item) => new VendorList(item.StringId, item.Name) },
                { ItemType.BuildingPart, (item) => new BuildingPart(item.StringId, item.Name) },
                { ItemType.AiTask, (item) => new AiTask(item.StringId, item.Name) },
                { ItemType.Container, (item) => new Container(item.StringId, item.Name) },
                { ItemType.MaterialSpecsClothing, (item) => new MaterialSpecsClothing(item.StringId, item.Name) },
                { ItemType.WordSwaps, (item) => new WordSwaps(item.StringId, item.Name) },
                { ItemType.DialoguePackage, (item) => new DialoguePackage(item.StringId, item.Name) },
                { ItemType.Weapon, (item) => new Weapon(item.StringId, item.Name) },
                { ItemType.Character, (item) => new Character(item.StringId, item.Name) },
                { ItemType.MaterialSpec, (item) => new MaterialSpec(item.StringId, item.Name) },
                { ItemType.Town, (item) => new Town(item.StringId, item.Name) },
                { ItemType.SquadTemplate, (item) => new Squad(item.StringId, item.Name) },
                { ItemType.AnimalCharacter, (item) => new AnimalCharacter(item.StringId, item.Name) },
                { ItemType.WeaponManufacturer, (item) => new WeaponManufacturer(item.StringId, item.Name) },
                { ItemType.MaterialSpecsWeapon, (item) => new MaterialSpecsWeapon(item.StringId, item.Name) },
                { ItemType.Stats, (item) => new Stats(item.StringId, item.Name) },
                { ItemType.Faction, (item) => new Faction(item.StringId, item.Name) },
                { ItemType.Constants, (item) => new Constants(item.StringId, item.Name) },
                { ItemType.Animation, (item) => new Animation(item.StringId, item.Name) },
                { ItemType.Road, (item) => new Road(item.StringId, item.Name) },
                { ItemType.Armour, (item) => new Armour(item.StringId, item.Name) },
                { ItemType.Biomes, (item) => new Biomes(item.StringId, item.Name) },
                { ItemType.Personality, (item) => new Personality(item.StringId, item.Name) },
                { ItemType.ColorData, (item) => new ColorData(item.StringId, item.Name) },
                { ItemType.ItemPlacementGroup, (item) => new ItemPlacementGroup(item.StringId, item.Name) },
                { ItemType.Light, (item) => new Light(item.StringId, item.Name) },
                { ItemType.CombatTechnique, (item) => new CombatTechnique(item.StringId, item.Name) },
                { ItemType.AiPackage, (item) => new AiPackage(item.StringId, item.Name) },
                { ItemType.Camera, (item) => new Camera(item.StringId, item.Name) },
                { ItemType.FoliageMesh, (item) => new FoliageMesh(item.StringId, item.Name) },
                { ItemType.FoliageLayer, (item) => new FoliageLayer(item.StringId, item.Name) },
                { ItemType.MapFeatures, (item) => new MapFeatures(item.StringId, item.Name) },
                { ItemType.Research, (item) => new Research(item.StringId, item.Name) },
                { ItemType.BuildingFunctionality, (item) => new BuildingFunctionality(item.StringId, item.Name) },
                { ItemType.FoliageBuilding, (item) => new FoliageBuilding(item.StringId, item.Name) },
                { ItemType.FactionTemplate, (item) => new FactionTemplate(item.StringId, item.Name) },
                { ItemType.FactionCampaign, (item) => new FactionCampaign(item.StringId, item.Name) },
                { ItemType.Grass, (item) => new Grass(item.StringId, item.Name) },
                { ItemType.EnvironmentResources, (item) => new EnvironmentResources(item.StringId, item.Name) },
                { ItemType.WorldEventState, (item) => new WorldEventState(item.StringId, item.Name) },
                { ItemType.Effect, (item) => new Effect(item.StringId, item.Name) },
                { ItemType.Race, (item) => new Race(item.StringId, item.Name) },
                { ItemType.Head, (item) => new Head(item.StringId, item.Name) },
                { ItemType.ItemsCulture, (item) => new ItemsCulture(item.StringId, item.Name) },
                { ItemType.Season, (item) => new Season(item.StringId, item.Name) },
                { ItemType.MapItem, (item) => new MapItem(item.StringId, item.Name) },
                { ItemType.NewGameStartoff, (item) => new NewGameStartoff(item.StringId, item.Name) },
                { ItemType.BuildingsSwap, (item) => new BuildingsSwap(item.StringId, item.Name) },
                { ItemType.AnimationFile, (item) => new AnimationFile(item.StringId, item.Name) },
                { ItemType.RaceGroup, (item) => new RaceGroup(item.StringId, item.Name) },
                { ItemType.NestItem, (item) => new NestItem(item.StringId, item.Name) },
                { ItemType.DaySchedule, (item) => new DaySchedule(item.StringId, item.Name) },
                { ItemType.BiomeGroup, (item) => new BiomeGroup(item.StringId, item.Name) },
                { ItemType.FarmData, (item) => new FarmData(item.StringId, item.Name) },
                { ItemType.FarmPart, (item) => new FarmPart(item.StringId, item.Name) },
                { ItemType.Attachment, (item) => new Attachment(item.StringId, item.Name) },
                { ItemType.DiplomaticAssaults, (item) => new DiplomaticAssaults(item.StringId, item.Name) },
                { ItemType.SingleDiplomaticAssault, (item) => new SingleDiplomaticAssault(item.StringId, item.Name) },
                { ItemType.GunData, (item) => new GunData(item.StringId, item.Name) },
                { ItemType.AnimalAnimation, (item) => new AnimalAnimation(item.StringId, item.Name) },
                { ItemType.UniqueSquadTemplate, (item) => new UniqueSquadTemplate(item.StringId, item.Name) },
                { ItemType.RepeatableBuildingPartSlot, (item) => new RepeatableBuildingPartSlot(item.StringId, item.Name) },
                { ItemType.MaterialSpecsCollection, (item) => new MaterialSpecsCollection(item.StringId, item.Name) },
                { ItemType.EffectFogVolume, (item) => new EffectFogVolume(item.StringId, item.Name) },
                { ItemType.WildlifeBirds, (item) => new WildlifeBirds(item.StringId, item.Name) },
                { ItemType.Weather, (item) => new Weather(item.StringId, item.Name) },
                { ItemType.Artifacts, (item) => new Artifacts(item.StringId, item.Name) },
                { ItemType.CharacterPhysicsAttachment, (item) => new CharacterPhysicsAttachment(item.StringId, item.Name) },
                { ItemType.Crossbow, (item) => new Crossbow(item.StringId, item.Name) },
                { ItemType.AmbientSound, (item) => new AmbientSound(item.StringId, item.Name) },
                { ItemType.AnimationEvent, (item) => new AnimationEvent(item.StringId, item.Name) },
                { ItemType.LimbReplacement, (item) => new LimbReplacement(item.StringId, item.Name) },
            };
        }
    }
}