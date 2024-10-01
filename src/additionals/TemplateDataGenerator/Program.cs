using TemplateDataGenerator;

if (Directory.Exists("Templates"))
{
    Directory.Delete("Templates", true);
}

Directory.CreateDirectory("Templates");


using var client = new WikiClientWrapper();
await client.Initialize();

var parser = new TemplateParser();
var analyzer = new TemplateAnalyzer(client, parser);

await analyzer.AnalyzePages();
