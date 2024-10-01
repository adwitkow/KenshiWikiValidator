using System.Text.Json.Serialization;

namespace TemplateDataGenerator.Template
{
    public class TemplateData
    {
        [JsonPropertyName("description")]
        public required string Description { get; init; }

        [JsonPropertyName("params")]
        public required Dictionary<string, TemplateParameter> Parameters { get; init; }

        [JsonPropertyName("paramOrder")]
        public required IEnumerable<string> ParameterOrder { get; init; }

        [JsonPropertyName("sets")]
        public required IEnumerable<ParameterSet> ParameterSets { get; init; }

        [JsonPropertyName("format")]
        public required string Format { get; init; }
    }
}
