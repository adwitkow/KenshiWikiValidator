// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
using System.Text;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

Console.WriteLine("Hello, World!");

var contextProvider = new ContextProvider();
var context = contextProvider.GetDataMiningContext();
var repo = new ItemRepository(context);
repo.Load();

var types = new[] { "Katana", "Sabre", "Blunt", "Heavy", "Hackers", "Unarmed", "Bow", "Turret", "Polearms" };

var weaponsByType = repo.GetItems<Weapon>()
    .GroupBy(weapon => weapon.SkillCategory);

foreach (var group in weaponsByType)
{
    var weaponType = group.Key.GetValueOrDefault();
    if (weaponType >= types.Length)
    {
        continue;
    }
    Console.WriteLine($"=={types[weaponType]}==");
    Console.WriteLine(@"
{| class=""article-table sortable"" style=""text-align: center""
! Icon
! Name
! Cut multiplier
! Blunt multiplier
! Blood loss
! Armour penetration
! Attack
! Defense
! Indoors
! Reach
! Additional bonuses");
    foreach (var weapon in group.OrderBy(w => w.Name))
    {
        Console.WriteLine(@$"|-
| [[File:{weapon.Name}.png|200x64px]]
| [[{weapon.Name}]]
| {weapon.CutDamageMultiplier}x
| {weapon.BluntDamageMultiplier}x
| {weapon.BleedMultiplier}x
| {GetPercentageValue(weapon.ArmourPenetration)}
| {GetPlusMinusValue(weapon.AttackModifier)}
| {GetPlusMinusValue(weapon.DefenceModifier)}
| {GetPlusMinusValue(weapon.IndoorsModifier)}
| {weapon.Length}");
        var additionals = GetAdditionals(weapon);
        if (!string.IsNullOrEmpty(additionals))
        {
            Console.WriteLine($@"|
<ul style=""margin-bottom: 2px; text-align: left"">
{additionals.Trim()}
</ul>");
        }
        else
        {
            Console.WriteLine("|");
        }
    }
    Console.WriteLine("|}");
}

string GetAdditionals(Weapon weapon)
{
    var builder = new StringBuilder();

    AppendBaseAdditional(builder, "Damage vs animals", weapon.AnimalDamageMultiplier);
    AppendBaseAdditional(builder, "Damage vs robots", weapon.RobotDamageMultiplier);
    AppendBaseAdditional(builder, "Damage vs humans", weapon.HumanDamageMulitplier);
    AppendComplexAdditional(builder, weapon, "Damage vs Spider", "Spider");
    AppendComplexAdditional(builder, weapon, "Damage vs Small Spider", "Small Spider");
    AppendComplexAdditional(builder, weapon, "Damage vs Bonedog", "Bonedog");
    AppendComplexAdditional(builder, weapon, "Damage vs Skimmer", "Skimmer");
    AppendComplexAdditional(builder, weapon, "Damage vs Beak Thing", "Beak Thing");
    AppendComplexAdditional(builder, weapon, "Damage vs Gorillo", "Gorillo");
    AppendComplexAdditional(builder, weapon, "Damage vs Leviathan", "Leviathan");
    
    if (weapon.InventoryFootprintHeight == 1 && weapon.InventoryFootprintWidth < 8)
    {
        builder.AppendLine("<li>Can be used in the secondary weapon slot</li>");
    }

    return builder.ToString();
}

void AppendBaseAdditional(StringBuilder builder, string header, float? value)
{
    if (value != 1)
    {
        builder.AppendLine($"<li>'''{header}''': {GetPercentageValue(value - 1)}</li>");
    }
}

void AppendComplexAdditional(StringBuilder builder, Weapon weapon, string header, string race)
{
    var exists = weapon.RaceDamage.Any(reference => reference.Item.Name == race);
    if (exists)
    {
        // dealing with generic default struct is a bigger headache than just doing the lookup twice
        // (even though it breaks my soul)
        var raceDamage = weapon.RaceDamage.FirstOrDefault(reference => reference.Item.Name == race);
        builder.AppendLine($"<li>'''{header}''': {GetPlusMinusValue(raceDamage.Value0)}%</li>");
    }
}

string GetPlusMinusValue(int? value)
{
    if (value is null)
    {
        return string.Empty;
    }

    if (value > 0)
    {
        return $"+{value}";
    }
    else
    {
        return value.ToString();
    }
}

string GetPercentageValue(float? value)
{
    if (value is null)
    {
        return string.Empty;
    }

    var percentage = (int)Math.Round(value.Value * 100);

    return GetPlusMinusValue(percentage) + "%";
}