using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Stats : ItemBase
    {
        public Stats(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.Stats;

        [Value("armour smith")]
        public float? ArmourSmith { get; set; }

        [Value("assassin")]
        public float? Assassin { get; set; }

        [Value("athletics")]
        public float? Athletics { get; set; }

        [Value("attack")]
        public float? Attack { get; set; }

        [Value("blunt")]
        public float? Blunt { get; set; }

        [Value("bow")]
        public float? Bow { get; set; }

        [Value("bow smith")]
        public float? BowSmith { get; set; }

        [Value("cooking")]
        public float? Cooking { get; set; }

        [Value("defence")]
        public float? Defence { get; set; }

        [Value("dexterity")]
        public float? Dexterity { get; set; }

        [Value("dodge")]
        public float? Dodge { get; set; }

        [Value("engineer")]
        public float? Engineer { get; set; }

        [Value("farming")]
        public float? Farming { get; set; }

        [Value("ff")]
        public float? Ff { get; set; }

        [Value("hackers")]
        public float? Hackers { get; set; }

        [Value("heavy weapons")]
        public float? HeavyWeapons { get; set; }

        [Value("katana")]
        public float? Katana { get; set; }

        [Value("labouring")]
        public float? Labouring { get; set; }

        [Value("lockpicking")]
        public float? Lockpicking { get; set; }

        [Value("medic")]
        public float? Medic { get; set; }

        [Value("perception")]
        public float? Perception { get; set; }

        [Value("poles")]
        public float? Poles { get; set; }

        [Value("robotics")]
        public float? Robotics { get; set; }

        [Value("sabres")]
        public float? Sabres { get; set; }

        [Value("science")]
        public float? Science { get; set; }

        [Value("stealth")]
        public float? Stealth { get; set; }

        [Value("strength")]
        public float? Strength { get; set; }

        [Value("swimming")]
        public float? Swimming { get; set; }

        [Value("thievery")]
        public float? Thievery { get; set; }

        [Value("toughness2")]
        public float? Toughness2 { get; set; }

        [Value("turrets")]
        public float? Turrets { get; set; }

        [Value("unarmed")]
        public float? Unarmed { get; set; }

        [Value("weapon smith")]
        public float? WeaponSmith { get; set; }

        [Value("endurance")]
        public float? Endurance { get; set; }

        [Value("evasion")]
        public float? Evasion { get; set; }

        [Value("fitness")]
        public float? Fitness { get; set; }

        [Value("intelligence")]
        public float? Intelligence { get; set; }

        [Value("labour")]
        public float? Labour { get; set; }

        [Value("mass combat")]
        public float? MassCombat { get; set; }

        [Value("move speed")]
        public float? MoveSpeed { get; set; }

        [Value("speed")]
        public float? Speed { get; set; }

        [Value("sword defence")]
        public float? SwordDefence { get; set; }

        [Value("sword skill")]
        public float? SwordSkill { get; set; }

        [Value("warrior spirit")]
        public float? WarriorSpirit { get; set; }

        [Value("xp")]
        public float? Xp { get; set; }

        [Value("free attribute points")]
        public int? FreeAttributePoints { get; set; }

        [Value("free skill points")]
        public int? FreeSkillPoints { get; set; }

        [Value("flowing")]
        public float? Flowing { get; set; }

        [Value("sword skill attack")]
        public float? SwordSkillAttack { get; set; }

        [Value("sword skill defence")]
        public float? SwordSkillDefence { get; set; }

    }
}