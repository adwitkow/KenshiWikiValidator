using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Biomes : ItemBase
    {
        public Biomes(string stringId, string name)
            : base(stringId, name)
        {
            this.Foliage = Enumerable.Empty<ItemReference<FoliageLayer>>();
            this.GroundEffects = Enumerable.Empty<ItemReference<Effect>>();
            this.WaterEffects = Enumerable.Empty<ItemReference<Effect>>();
            this.HomelessSpawns = Enumerable.Empty<ItemReference<Squad>>();
            this.Nests = Enumerable.Empty<ItemReference<Town>>();
            this.GroundEffect = Enumerable.Empty<ItemReference<Effect>>();
        }

        public override ItemType Type => ItemType.Biomes;

        [Value("absorbance 0")]
        public float? Absorbance0 { get; set; }

        [Value("absorbance 1")]
        public float? Absorbance1 { get; set; }

        [Value("absorbance 2")]
        public float? Absorbance2 { get; set; }

        [Value("absorbance dirt")]
        public float? AbsorbanceDirt { get; set; }

        [Value("absorbance grass")]
        public float? AbsorbanceGrass { get; set; }

        [Value("absorbance road")]
        public float? AbsorbanceRoad { get; set; }

        [Value("brightness fix")]
        public float? BrightnessFix { get; set; }

        [Value("distort amplitude")]
        public float? DistortAmplitude { get; set; }

        [Value("distort wavelength")]
        public float? DistortWavelength { get; set; }

        [Value("fade distance")]
        public float? FadeDistance { get; set; }

        [Value("overlay mult dirt")]
        public float? OverlayMultDirt { get; set; }

        [Value("overlay mult grass")]
        public float? OverlayMultGrass { get; set; }

        [Value("overlay mult road")]
        public float? OverlayMultRoad { get; set; }

        [Value("overlay mult vertical")]
        public float? OverlayMultVertical { get; set; }

        [Value("scum distortion")]
        public float? ScumDistortion { get; set; }

        [Value("scum scale X")]
        public float? ScumScaleX { get; set; }

        [Value("scum scale Y")]
        public float? ScumScaleY { get; set; }

        [Value("slope fade 1")]
        public float? SlopeFade1 { get; set; }

        [Value("slope fade 2")]
        public float? SlopeFade2 { get; set; }

        [Value("slope fade grass")]
        public float? SlopeFadeGrass { get; set; }

        [Value("slope max 1")]
        public float? SlopeMax1 { get; set; }

        [Value("slope max 2")]
        public float? SlopeMax2 { get; set; }

        [Value("slope max grass")]
        public float? SlopeMaxGrass { get; set; }

        [Value("slope min 1")]
        public float? SlopeMin1 { get; set; }

        [Value("slope min 2")]
        public float? SlopeMin2 { get; set; }

        [Value("slope min grass")]
        public float? SlopeMinGrass { get; set; }

        [Value("sun brightness")]
        public float? SunBrightness { get; set; }

        [Value("tiling X 0")]
        public float? TilingX0 { get; set; }

        [Value("tiling X 1")]
        public float? TilingX1 { get; set; }

        [Value("tiling X 2")]
        public float? TilingX2 { get; set; }

        [Value("tiling X dirt")]
        public float? TilingXDirt { get; set; }

        [Value("tiling X grass")]
        public float? TilingXGrass { get; set; }

        [Value("tiling X road")]
        public float? TilingXRoad { get; set; }

        [Value("tiling Y 0")]
        public float? TilingY0 { get; set; }

        [Value("tiling Y 1")]
        public float? TilingY1 { get; set; }

        [Value("tiling Y 2")]
        public float? TilingY2 { get; set; }

        [Value("tiling Y dirt")]
        public float? TilingYDirt { get; set; }

        [Value("tiling Y grass")]
        public float? TilingYGrass { get; set; }

        [Value("tiling Y road")]
        public float? TilingYRoad { get; set; }

        [Value("water distortion")]
        public float? WaterDistortion { get; set; }

        [Value("water gloss")]
        public float? WaterGloss { get; set; }

        [Value("water glow")]
        public float? WaterGlow { get; set; }

        [Value("water scale X")]
        public float? WaterScaleX { get; set; }

        [Value("water scale Y")]
        public float? WaterScaleY { get; set; }

        [Value("water strength")]
        public float? WaterStrength { get; set; }

        [Value("water visibility")]
        public float? WaterVisibility { get; set; }

        [Value("ambient light")]
        public int? AmbientLight { get; set; }

        [Value("dirt type")]
        public int? DirtType { get; set; }

        [Value("grass type")]
        public int? GrassType { get; set; }

        [Value("ground colour")]
        public int? GroundColour { get; set; }

        [Value("ground type")]
        public int? GroundType { get; set; }

        [Value("index")]
        public int? Index { get; set; }

        [Value("road type")]
        public int? RoadType { get; set; }

        [Value("slope type")]
        public int? SlopeType { get; set; }

        [Value("water color")]
        public int? WaterColor { get; set; }

        [Value("texture base")]
        public object? TextureBase { get; set; }

        [Value("texture base normal")]
        public object? TextureBaseNormal { get; set; }

        [Value("texture dirt")]
        public object? TextureDirt { get; set; }

        [Value("texture dirt normal")]
        public object? TextureDirtNormal { get; set; }

        [Value("texture grass")]
        public object? TextureGrass { get; set; }

        [Value("texture grass normal")]
        public object? TextureGrassNormal { get; set; }

        [Value("texture road")]
        public object? TextureRoad { get; set; }

        [Value("texture road normal")]
        public object? TextureRoadNormal { get; set; }

        [Value("texture scum")]
        public object? TextureScum { get; set; }

        [Value("texture scum normal")]
        public object? TextureScumNormal { get; set; }

        [Value("texture slope")]
        public object? TextureSlope { get; set; }

        [Value("texture slope normal")]
        public object? TextureSlopeNormal { get; set; }

        [Value("texture vertical")]
        public object? TextureVertical { get; set; }

        [Value("texture vertical normal")]
        public object? TextureVerticalNormal { get; set; }

        [Value("turbulence map")]
        public object? TurbulenceMap { get; set; }

        [Value("water normal")]
        public object? WaterNormal { get; set; }

        [Reference("foliage")]
        public IEnumerable<ItemReference<FoliageLayer>> Foliage { get; set; }

        [Reference("ground effects")]
        public IEnumerable<ItemReference<Effect>> GroundEffects { get; set; }

        [Reference("water effects")]
        public IEnumerable<ItemReference<Effect>> WaterEffects { get; set; }

        [Reference("homeless spawns")]
        public IEnumerable<ItemReference<Squad>> HomelessSpawns { get; set; }

        [Reference("nests")]
        public IEnumerable<ItemReference<Town>> Nests { get; set; }

        [Reference("ground effect")]
        public IEnumerable<ItemReference<Effect>> GroundEffect { get; set; }

    }
}