using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators.Rules
{
    public class ArticleData
    {
        private readonly Dictionary<string, string> data;

        public ArticleData()
        {
            this.data = new Dictionary<string, string>();
        }

        public string? Get(string key)
        {
            this.data.TryGetValue(key, out var value);

            return value;
        }

        public void Add(string key, string value)
        {
            this.data.Add(key, value);
        }
    }
}
