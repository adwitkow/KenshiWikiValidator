using System.Text.Json.Serialization;

namespace TemplateAnalyzer.Template
{
    internal class TemplateParameter
    {
        [JsonPropertyName("label")]
        public required string Label { get; init; }

        [JsonPropertyName("description")]
        public required string Description { get; init; }

        [JsonPropertyName("required")]
        public required bool Required { get; init; }

        [JsonPropertyName("suggested")]
        public required bool Suggested { get; init; }

        [JsonPropertyName("deprecated")]
        public required string Deprecated { get; init; }

        [JsonPropertyName("aliases")]
        public required IEnumerable<string> Aliases { get; init; }

        [JsonPropertyName("default")]
        public required string Default { get; init; }

        [JsonPropertyName("autovalue")]
        public required string AutoValue { get; init; }

        [JsonPropertyName("example")]
        public required string Example { get; init; }

        [JsonPropertyName("type")]
        public required string Type { get; init; }

        [JsonPropertyName("inherits")]
        public required string Inherits { get; init; }

        [JsonPropertyName("suggestedvalues")]
        public required IEnumerable<string> SuggestedValues { get; init; }
    }
}
