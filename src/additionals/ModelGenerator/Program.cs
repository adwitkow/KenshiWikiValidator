// See https://aka.ms/new-console-template for more information
using KenshiWikiValidator.OcsProxy;
using OpenConstructionSet.Mods;
using System.Text;

var primitives = new Dictionary<string, string>()
{
    { "Boolean", "bool?" },
    { "Single", "float?" },
    { "String", "string?" },
    { "Int32", "int?" },
    { "FileValue", "object?" },
};

var repository = new ModelGeneratorItemRepository();
repository.Load();

var itemsByType = repository.GetDataItems()
    .GroupBy(item => item.Type);

var output = "output";
if (Directory.Exists(output))
{
    Console.WriteLine("Clearing the output directory...");
    Directory.Delete(output, true);
}

Console.WriteLine();
foreach (var itemTypeGroup in itemsByType)
{
    Console.WriteLine($"{{ ItemType.{itemTypeGroup.Key}, (item) => new {itemTypeGroup.Key}(item.StringId, item.Name) }},");

    var props = new Dictionary<string, object>();
    var categories = new Dictionary<string, ModItem>();
    foreach (var item in itemTypeGroup)
    {
        foreach (var prop in item.Values)
        {
            if (props.ContainsKey(prop.Key))
            {
                continue;
            }

            props.Add(prop.Key, prop.Value);
        }

        foreach (var refCategory in item.ReferenceCategories)
        {
            if (!refCategory.References.Any() || categories.ContainsKey(refCategory.Key))
            {
                continue;
            }

            var firstItem = repository.GetDataItemByStringId(refCategory.References.FirstOrDefault().TargetId);

            categories.Add(refCategory.Key, firstItem);
        }
    }

    var builder = new StringBuilder();

    builder.AppendLine(@$"using OpenConstructionSet.Data;
using OpenConstructionSet.Mods;

namespace KenshiWikiValidator.OcsProxy.NamespaceToFix
{{
    public class {itemTypeGroup.Key} : ItemBase
    {{
        public {itemTypeGroup.Key}(ModItem item)
            : base(item)
        {{");
    
    foreach (var category in categories)
    {
        builder.AppendLine($"            this.{ToPropertyName(category.Key)} = Enumerable.Empty<ItemReference<{category.Value.Type}>>();");
    }

    builder.AppendLine(@$"        }}

        public override ItemType Type => ItemType.{itemTypeGroup.Key};
");

    foreach (var prop in props)
    {
        builder.AppendLine($"        [Value(\"{prop.Key}\")]");
        builder.AppendLine($"        public {ConvertPrimitive(prop)} {ToPropertyName(prop.Key)} {{ get; set; }}");
        builder.AppendLine();
    }

    foreach (var refCategory in categories)
    {
        builder.AppendLine($"        [Reference(\"{refCategory.Key}\")]");
        builder.AppendLine($"        public IEnumerable<ItemReference<{refCategory.Value.Type}>> {ToPropertyName(refCategory.Key)} {{ get; set; }}");
        builder.AppendLine();
    }

    builder.AppendLine("    }");
    builder.Append('}');

    Directory.CreateDirectory(output);
    File.WriteAllText(Path.Combine(output, $"{itemTypeGroup.Key}.cs"), builder.ToString());
}

Console.WriteLine("Finished.");

static string ToPropertyName(string valueName)
{
    var segments = valueName.ToLower().Split(' ');
    return string.Join("", segments.Select(seg => string.Concat(seg[..1].ToUpper(), seg.AsSpan(1))));
}

string ConvertPrimitive(KeyValuePair<string, object> prop)
{
    return primitives[prop.Value.GetType().Name];
}