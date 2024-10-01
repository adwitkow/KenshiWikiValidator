using System.Text.Json;
using WikiClientLibrary.Pages;

namespace TemplateDataGenerator
{
    public class TemplateAnalyzer
    {
        private readonly WikiClientWrapper _client;
        private readonly TemplateParser _parser;

        public TemplateAnalyzer(WikiClientWrapper client, TemplateParser parser)
        {
            _client = client;
            _parser = parser;
        }

        public async Task AnalyzePages()
        {
            var pages = _client.EnumerateTemplatePagesAsync();

            var documentationPages = new List<WikiPage>();
            var pagesToAnalyze = new List<TemplatePage>();
            await foreach (var page in pages)
            {
                var lowercaseTitle = page.Title.ToLower();
                if (lowercaseTitle.EndsWith("/sandbox") || lowercaseTitle.EndsWith("/testcases"))
                {
                    continue;
                }

                if (lowercaseTitle.EndsWith("/doc"))
                {
                    documentationPages.Add(page);
                    continue;
                }

                Console.Write($"Mapping {page.Title}...");
                if (page.Title is "Template:Join"
                    or "Template:Reflist"
                    or "Template:Navbox"
                    or "Template:Navbox2")
                {
                    Console.WriteLine("Skipped!");
                    continue;
                }

                Console.Write("Counting transclusions...");

                var transclusions = await _client.CountTransclusions(page.Title);

                Console.Write("Getting the page content...");

                await page.RefreshAsync(PageQueryOptions.FetchContent);

                var templatePage = new TemplatePage()
                {
                    Title = page.Title,
                    Content = page.Content,
                    Transclusions = transclusions,
                };

                pagesToAnalyze.Add(templatePage);
                Console.WriteLine("Done!");
            }

            foreach (var documentationPage in documentationPages)
            {
                Console.WriteLine($"Assigning documentation page {documentationPage.Title}...");
                await documentationPage.RefreshAsync(PageQueryOptions.FetchContent);

                var result = new TemplatePage()
                {
                    Title = documentationPage.Title,
                    Content = documentationPage.Content,
                };

                var parentName = documentationPage.Title.Split('/').First();
                var parent = pagesToAnalyze.SingleOrDefault(page => page.Title == parentName);

                if (parent is not null)
                {
                    parent.DocumentationPage = result;
                }
            }

            var ordered = pagesToAnalyze.OrderByDescending(page => page.Transclusions)
                .ThenBy(page => page.Title);
            foreach (var page in ordered)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{page.Transclusions}\t{page.Title}");

                if (page.DocumentationPage is null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\tDocumentation is missing");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\tDocumentation is present");
                }

                if (!page.Content.Contains("<templatedata>"))
                {
                    if (page.DocumentationPage is not null)
                    {
                        if (!page.DocumentationPage.Content.Contains("<templatedata>"))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\tTemplateData is missing");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("\tTemplateData is present");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\tTemplateData is missing");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\tTemplateData is present");
                }

                var templateData = _parser.ParseTemplate(page.Content);
                var serializedData = JsonSerializer.Serialize(templateData, new JsonSerializerOptions()
                {
                    WriteIndented = true,
                });
                var cleanTitle = page.Title.Replace("/", "").Replace("Template:", "");
                File.WriteAllText(Path.Combine("Templates", $"TemplateData-{cleanTitle}.txt"), serializedData);

                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Finished listing {ordered.Count()} template pages.");
        }
    }
}
