using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiWikiValidator.Features.WikiSections
{
    public class WikiSection
    {
        public WikiSection()
        {
            this.Header = string.Empty;
            this.Components = new List<string>();
        }

        public string Header { get; set; }

        public List<string> Components { get; }
    }
}
