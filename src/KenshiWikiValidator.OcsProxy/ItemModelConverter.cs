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
using OpenConstructionSet.Data;
using OpenConstructionSet.Mods;

namespace KenshiWikiValidator.OcsProxy
{
    public class ItemModelConverter
    {
        private readonly Dictionary<ItemType, Func<ModItem, IItem>> conversionMap;
        private readonly ItemMapper mapper;

        public ItemModelConverter(IItemRepository itemRepository)
        {
            this.conversionMap = this.CreateConversionMap();
            this.mapper = new ItemMapper(itemRepository);
        }

        public IItem Convert(ModItem item)
        {
            var result = this.conversionMap[item.Type].Invoke(item);

            return result;
        }

        public IEnumerable<(ModItem baseItem, IItem result)> Convert(IEnumerable<ModItem> contextItems)
        {
            return contextItems.Select(baseItem => (baseItem, this.Convert(baseItem)));
        }

        public IItem MapProperties((ModItem Base, IItem Result) convertedPair)
        {
            return this.mapper.Map(convertedPair.Base, convertedPair.Result);
        }

        private Dictionary<ItemType, Func<ModItem, IItem>> CreateConversionMap()
        {
            return new Dictionary<ItemType, Func<ModItem, IItem>>()
            {
                { ItemType.LocationalDamage, (item) => new LocationalDamage(item) },
                { ItemType.Item, (item) => new Models.Item(item) },
                { ItemType.Building, (item) => new Building(item) },
                { ItemType.DialogueLine, (item) => new DialogueLine(item) },
                { ItemType.DialogAction, (item) => new DialogAction(item) },
                { ItemType.Dialogue, (item) => new Dialogue(item) },
                { ItemType.VendorList, (item) => new VendorList(item) },
                { ItemType.BuildingPart, (item) => new BuildingPart(item) },
                { ItemType.AiTask, (item) => new AiTask(item) },
                { ItemType.Container, (item) => new Container(item) },
                { ItemType.MaterialSpecsClothing, (item) => new MaterialSpecsClothing(item) },
                { ItemType.WordSwaps, (item) => new WordSwaps(item) },
                { ItemType.DialoguePackage, (item) => new DialoguePackage(item) },
                { ItemType.Weapon, (item) => new Weapon(item) },
                { ItemType.Character, (item) => new Character(item) },
                { ItemType.MaterialSpec, (item) => new MaterialSpec(item) },
                { ItemType.Town, (item) => new Town(item) },
                { ItemType.SquadTemplate, (item) => new Squad(item) },
                { ItemType.AnimalCharacter, (item) => new AnimalCharacter(item) },
                { ItemType.WeaponManufacturer, (item) => new WeaponManufacturer(item) },
                { ItemType.MaterialSpecsWeapon, (item) => new MaterialSpecsWeapon(item) },
                { ItemType.Stats, (item) => new Stats(item) },
                { ItemType.Faction, (item) => new Faction(item) },
                { ItemType.Constants, (item) => new Constants(item) },
                { ItemType.Animation, (item) => new Animation(item) },
                { ItemType.Road, (item) => new Road(item) },
                { ItemType.Armour, (item) => new Armour(item) },
                { ItemType.Biomes, (item) => new Biomes(item) },
                { ItemType.Personality, (item) => new Personality(item) },
                { ItemType.ColorData, (item) => new ColorData(item) },
                { ItemType.ItemPlacementGroup, (item) => new ItemPlacementGroup(item) },
                { ItemType.Light, (item) => new Light(item) },
                { ItemType.CombatTechnique, (item) => new CombatTechnique(item) },
                { ItemType.AiPackage, (item) => new AiPackage(item) },
                { ItemType.Camera, (item) => new Camera(item) },
                { ItemType.FoliageMesh, (item) => new FoliageMesh(item) },
                { ItemType.FoliageLayer, (item) => new FoliageLayer(item) },
                { ItemType.MapFeatures, (item) => new MapFeatures(item) },
                { ItemType.Research, (item) => new Research(item) },
                { ItemType.BuildingFunctionality, (item) => new BuildingFunctionality(item) },
                { ItemType.FoliageBuilding, (item) => new FoliageBuilding(item) },
                { ItemType.FactionTemplate, (item) => new FactionTemplate(item) },
                { ItemType.FactionCampaign, (item) => new FactionCampaign(item) },
                { ItemType.Grass, (item) => new Grass(item) },
                { ItemType.EnvironmentResources, (item) => new EnvironmentResources(item) },
                { ItemType.WorldEventState, (item) => new WorldEventState(item) },
                { ItemType.Effect, (item) => new Effect(item) },
                { ItemType.Race, (item) => new Race(item) },
                { ItemType.Head, (item) => new Head(item) },
                { ItemType.ItemsCulture, (item) => new ItemsCulture(item) },
                { ItemType.Season, (item) => new Season(item) },
                { ItemType.MapItem, (item) => new MapItem(item) },
                { ItemType.NewGameStartoff, (item) => new NewGameStartoff(item) },
                { ItemType.BuildingsSwap, (item) => new BuildingsSwap(item) },
                { ItemType.AnimationFile, (item) => new AnimationFile(item) },
                { ItemType.RaceGroup, (item) => new RaceGroup(item) },
                { ItemType.NestItem, (item) => new NestItem(item) },
                { ItemType.DaySchedule, (item) => new DaySchedule(item) },
                { ItemType.BiomeGroup, (item) => new BiomeGroup(item) },
                { ItemType.FarmData, (item) => new FarmData(item) },
                { ItemType.FarmPart, (item) => new FarmPart(item) },
                { ItemType.Attachment, (item) => new Attachment(item) },
                { ItemType.DiplomaticAssaults, (item) => new DiplomaticAssaults(item) },
                { ItemType.SingleDiplomaticAssault, (item) => new SingleDiplomaticAssault(item) },
                { ItemType.GunData, (item) => new GunData(item) },
                { ItemType.AnimalAnimation, (item) => new AnimalAnimation(item) },
                { ItemType.UniqueSquadTemplate, (item) => new UniqueSquadTemplate(item) },
                { ItemType.RepeatableBuildingPartSlot, (item) => new RepeatableBuildingPartSlot(item) },
                { ItemType.MaterialSpecsCollection, (item) => new MaterialSpecsCollection(item) },
                { ItemType.EffectFogVolume, (item) => new EffectFogVolume(item) },
                { ItemType.WildlifeBirds, (item) => new WildlifeBirds(item) },
                { ItemType.Weather, (item) => new Weather(item) },
                { ItemType.Artifacts, (item) => new Artifacts(item) },
                { ItemType.CharacterPhysicsAttachment, (item) => new CharacterPhysicsAttachment(item) },
                { ItemType.Crossbow, (item) => new Crossbow(item) },
                { ItemType.AmbientSound, (item) => new AmbientSound(item) },
                { ItemType.AnimationEvent, (item) => new AnimationEvent(item) },
                { ItemType.LimbReplacement, (item) => new LimbReplacement(item) },
            };
        }
    }
}