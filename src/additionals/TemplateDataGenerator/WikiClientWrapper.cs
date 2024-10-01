using WikiClientLibrary;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Sites;

namespace TemplateDataGenerator
{
    public class WikiClientWrapper : IDisposable
    {
        private const string WikiApiUrl = "https://kenshi.fandom.com/api.php";

        private readonly WikiClient _client;
        private readonly WikiSite _site;

        public WikiClientWrapper()
        {
            _client = new WikiClient();
            _site = new WikiSite(_client, WikiApiUrl);
        }

        public async Task Initialize()
        {
            await _site.Initialization;
        }

        public IAsyncEnumerable<WikiPage> EnumerateTemplatePagesAsync()
        {
            var generator = new AllPagesGenerator(_site)
            {
                NamespaceId = BuiltInNamespaces.Template
            };

            return generator.EnumPagesAsync();
        }

        public ValueTask<int> CountTransclusions(string title)
        {
            var transGenerator = new TranscludedInGenerator(_site)
            {
                TargetTitle = title,
            };

            return transGenerator.EnumPagesAsync().CountAsync();
        }

        public void Dispose()
        {
            if (_client is not null)
            {
                _client.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
