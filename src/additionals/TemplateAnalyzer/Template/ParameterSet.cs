using System.Text.Json.Serialization;

namespace TemplateAnalyzer.Template
{
    internal class ParameterSet
    {
        [JsonPropertyName("label")]
        public required string Label { get; init; }

        [JsonPropertyName("params")]
        public required IEnumerable<string> Parameters { get; init; }
    }
}
