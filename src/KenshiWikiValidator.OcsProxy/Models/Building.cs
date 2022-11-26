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

using KenshiWikiValidator.OcsProxy.Models.Interfaces;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Building : ItemBase, IDescriptive
    {
        public Building(string stringId, string name)
            : base(stringId, name)
        {
            this.Parts = Enumerable.Empty<ItemReference<BuildingPart>>();
            this.Spawns = Enumerable.Empty<ItemReference<Squad>>();
            this.Construction = Enumerable.Empty<ItemReference<Item>>();
            this.Doors = Enumerable.Empty<ItemReference<Building>>();
            this.Interior = Enumerable.Empty<ItemReference<BuildingPart>>();
            this.InteriorMask = Enumerable.Empty<ItemReference<BuildingPart>>();
            this.Material = Enumerable.Empty<ItemReference<MaterialSpec>>();
            this.SharesInteriorsWith = Enumerable.Empty<ItemReference<Building>>();
            this.Functionality = Enumerable.Empty<ItemReference<BuildingFunctionality>>();
            this.LimitInventory = Enumerable.Empty<ItemReference<Item>>();
            this.Sounds = Enumerable.Empty<ItemReference<AmbientSound>>();
            this.UpgradesTo = Enumerable.Empty<ItemReference<Building>>();
            this.FarmData = Enumerable.Empty<ItemReference<FarmData>>();
            this.SnapsTo = Enumerable.Empty<ItemReference<Building>>();
            this.GunTurret = Enumerable.Empty<ItemReference<GunData>>();
            this.WallSubsections = Enumerable.Empty<ItemReference<Building>>();
            this.BirdAttractor = Enumerable.Empty<ItemReference<WildlifeBirds>>();
        }

        public override ItemType Type => ItemType.Building;

        [Value("allow animals")]
        public bool? AllowAnimals { get; set; }

        [Value("always locks")]
        public bool? AlwaysLocks { get; set; }

        [Value("cavernous")]
        public bool? Cavernous { get; set; }

        [Value("ceiling placement")]
        public bool? CeilingPlacement { get; set; }

        [Value("creates player town")]
        public bool? CreatesPlayerTown { get; set; }

        [Value("destroyed navmesh")]
        public bool? DestroyedNavmesh { get; set; }

        [Value("enforce ceiling")]
        public bool? EnforceCeiling { get; set; }

        [Value("has inventory")]
        public bool? HasInventory { get; set; }

        [Value("has lock")]
        public bool? HasLock { get; set; }

        [Value("interior terrain")]
        public bool? InteriorTerrain { get; set; }

        [Value("is exterior furniture")]
        public bool? IsExteriorFurniture { get; set; }

        [Value("is feature")]
        public bool? IsFeature { get; set; }

        [Value("is foliage")]
        public bool? IsFoliage { get; set; }

        [Value("is gateway")]
        public bool? IsGateway { get; set; }

        [Value("is interior furniture")]
        public bool? IsInteriorFurniture { get; set; }

        [Value("is node")]
        public bool? IsNode { get; set; }

        [Value("is shelter")]
        public bool? IsShelter { get; set; }

        [Value("is sign")]
        public bool? IsSign { get; set; }

        [Value("is wall gate")]
        public bool? IsWallGate { get; set; }

        [Value("links together")]
        public bool? LinksTogether { get; set; }

        [Value("match slope")]
        public bool? MatchSlope { get; set; }

        [Value("build speed mult")]
        public float? BuildSpeedMult { get; set; }

        [Value("build threshold")]
        public float? BuildThreshold { get; set; }

        [Value("building height")]
        public float? BuildingHeight { get; set; }

        [Value("door comes out")]
        public float? DoorComesOut { get; set; }

        [Value("door move dist")]
        public float? DoorMoveDist { get; set; }

        [Value("door move speed")]
        public float? DoorMoveSpeed { get; set; }

        [Value("hardness")]
        public float? Hardness { get; set; }

        [Value("interior integrity")]
        public float? InteriorIntegrity { get; set; }

        [Value("link length")]
        public float? LinkLength { get; set; }

        [Value("max slope")]
        public float? MaxSlope { get; set; }

        [Value("output rate")]
        public float? OutputRate { get; set; }

        [Value("scale")]
        public float? Scale { get; set; }

        [Value("stackable bonus mult")]
        public float? StackableBonusMult { get; set; }

        [Value("vertical position tolerance")]
        public float? VerticalPositionTolerance { get; set; }

        [Value("build materials")]
        public int? BuildMaterials { get; set; }

        [Value("desirability")]
        public int? Desirability { get; set; }

        [Value("door axis")]
        public int? DoorAxis { get; set; }

        [Value("door navmesh axis")]
        public int? DoorNavmeshAxis { get; set; }

        [Value("door type")]
        public int? DoorType { get; set; }

        [Value("exterior material")]
        public int? ExteriorMaterial { get; set; }

        [Value("interior ambience")]
        public int? InteriorAmbience { get; set; }

        [Value("interior material")]
        public int? InteriorMaterial { get; set; }

        [Value("itemtype limit")]
        public int? ItemtypeLimit { get; set; }

        [Value("link type")]
        public int? LinkType { get; set; }

        [Value("lock level bonus")]
        public int? LockLevelBonus { get; set; }

        [Value("max operators")]
        public int? MaxOperators { get; set; }

        [Value("node type")]
        public int? NodeType { get; set; }

        [Value("path mode")]
        public int? PathMode { get; set; }

        [Value("power capacity")]
        public int? PowerCapacity { get; set; }

        [Value("power output")]
        public int? PowerOutput { get; set; }

        [Value("select sound")]
        public int? SelectSound { get; set; }

        [Value("storage size height")]
        public int? StorageSizeHeight { get; set; }

        [Value("storage size width")]
        public int? StorageSizeWidth { get; set; }

        [Value("building category")]
        public string? BuildingCategory { get; set; }

        [Value("building group")]
        public string? BuildingGroup { get; set; }

        [Value("Description")]
        public string? Description { get; set; }

        [Value("inventory content name")]
        public string? InventoryContentName { get; set; }

        [Value("destroyed boundary")]
        public object? DestroyedBoundary { get; set; }

        [Value("distant mesh")]
        public object? DistantMesh { get; set; }

        [Value("icon")]
        public object? Icon { get; set; }

        [Value("sound x")]
        public float? SoundX { get; set; }

        [Value("sound y")]
        public float? SoundY { get; set; }

        [Value("sound z")]
        public float? SoundZ { get; set; }

        [Value("sfx loop")]
        public string? SfxLoop { get; set; }

        [Value("building category!")]
        public bool? BuildingCategory2 { get; set; }

        [Value("auto pathmesh")]
        public bool? AutoPathmesh { get; set; }

        [Value("is door")]
        public bool? IsDoor { get; set; }

        [Value("no collision")]
        public bool? NoCollision { get; set; }

        [Value("output per day")]
        public bool? OutputPerDay { get; set; }

        [Value("snap to me")]
        public bool? SnapToMe { get; set; }

        [Value("snap to me turrets")]
        public bool? SnapToMeTurrets { get; set; }

        [Value("store anything")]
        public bool? StoreAnything { get; set; }

        [Value("store armour")]
        public bool? StoreArmour { get; set; }

        [Value("store weapons")]
        public bool? StoreWeapons { get; set; }

        [Value("unwalkable")]
        public bool? Unwalkable { get; set; }

        [Value("build speed")]
        public float? BuildSpeed { get; set; }

        [Value("lock level mult")]
        public float? LockLevelMult { get; set; }

        [Value("production mult")]
        public float? ProductionMult { get; set; }

        [Value("scaling")]
        public float? Scaling { get; set; }

        [Value("snap to me X spacing")]
        public float? SnapToMeXSpacing { get; set; }

        [Value("snap to me Y")]
        public float? SnapToMeY { get; set; }

        [Value("snap to me Z")]
        public float? SnapToMeZ { get; set; }

        [Value("build time")]
        public int? BuildTime { get; set; }

        [Value("function")]
        public int? Function { get; set; }

        [Value("hulls offset axis")]
        public int? HullsOffsetAxis { get; set; }

        [Value("hulls radius")]
        public int? HullsRadius { get; set; }

        [Value("itemfunc limit")]
        public int? ItemfuncLimit { get; set; }

        [Value("max part repeats")]
        public int? MaxPartRepeats { get; set; }

        [Value("num pathmesh hulls")]
        public int? NumPathmeshHulls { get; set; }

        [Value("num turret repeats")]
        public int? NumTurretRepeats { get; set; }

        [Value("description")]
        public string? Description2 { get; set; }

        [Value("GUI")]
        public object? Gui { get; set; }

        [Reference("parts")]
        public IEnumerable<ItemReference<BuildingPart>> Parts { get; set; }

        [Reference("spawns")]
        public IEnumerable<ItemReference<Squad>> Spawns { get; set; }

        [Reference("construction")]
        public IEnumerable<ItemReference<Item>> Construction { get; set; }

        [Reference("doors")]
        public IEnumerable<ItemReference<Building>> Doors { get; set; }

        [Reference("interior")]
        public IEnumerable<ItemReference<BuildingPart>> Interior { get; set; }

        [Reference("interior mask")]
        public IEnumerable<ItemReference<BuildingPart>> InteriorMask { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpec>> Material { get; set; }

        [Reference("shares interiors with")]
        public IEnumerable<ItemReference<Building>> SharesInteriorsWith { get; set; }

        [Reference("functionality")]
        public IEnumerable<ItemReference<BuildingFunctionality>> Functionality { get; set; }

        [Reference("limit inventory")]
        public IEnumerable<ItemReference<Item>> LimitInventory { get; set; }

        [Reference("sounds")]
        public IEnumerable<ItemReference<AmbientSound>> Sounds { get; set; }

        [Reference("upgrades to")]
        public IEnumerable<ItemReference<Building>> UpgradesTo { get; set; }

        [Reference("farm data")]
        public IEnumerable<ItemReference<FarmData>> FarmData { get; set; }

        [Reference("snaps to")]
        public IEnumerable<ItemReference<Building>> SnapsTo { get; set; }

        [Reference("gun turret")]
        public IEnumerable<ItemReference<GunData>> GunTurret { get; set; }

        [Reference("wall subsections")]
        public IEnumerable<ItemReference<Building>> WallSubsections { get; set; }

        [Reference("bird attractor")]
        public IEnumerable<ItemReference<WildlifeBirds>> BirdAttractor { get; set; }
    }
}