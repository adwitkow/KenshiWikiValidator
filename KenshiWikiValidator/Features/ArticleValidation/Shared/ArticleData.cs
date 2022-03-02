﻿using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.ArticleValidation.Shared
{
    public class ArticleData
    {
        public ArticleData()
        {
            this.WikiTemplates = new List<WikiTemplate>();
            this.StringIds = new List<string>();
        }

        public ICollection<string> StringIds { get; set; }

        public IEnumerable<WikiTemplate> WikiTemplates { get; set; }
    }
}
