﻿// See https://aka.ms/new-console-template for more information
using KenshiWikiValidator.OcsProxy;
using OpenConstructionSet.Data.Models;
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

foreach (var itemTypeGroup in itemsByType)
{
    var props = new Dictionary<string, object>();
    var categories = new Dictionary<string, DataItem>();
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
            if (!refCategory.Value.Any() || categories.ContainsKey(refCategory.Key))
            {
                continue;
            }

            var firstItem = repository.GetDataItemByStringId(refCategory.Value.FirstOrDefault().Value.TargetId);

            categories.Add(refCategory.Key, firstItem);
        }
    }

    var builder = new StringBuilder();

    builder.AppendLine(@$"using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.NamespaceToFix
{{
    public class {itemTypeGroup.Key} : ItemBase
    {{
        public {itemTypeGroup.Key}(string stringId, string name)
            : base(stringId, name)
        {{");
    
    foreach (var category in categories)
    {
        builder.AppendLine($"            this.{ToPropertyName(category.Key)} = Enumerable.Empty<ItemReference<{category.Value.Type}>>");
    }

    builder.AppendLine(@$"        }}

        public override ItemType Type => ItemType.{itemTypeGroup.Key};");

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

static string ToPropertyName(string valueName)
{
    var segments = valueName.ToLower().Split(' ');
    return string.Join("", segments.Select(seg => string.Concat(seg[..1].ToUpper(), seg.AsSpan(1))));
}

string ConvertPrimitive(KeyValuePair<string, object> prop)
{
    return primitives[prop.Value.GetType().Name];
}