﻿namespace KenshiWikiValidator
{
    public class ArticleValidationResult
    {
        public ArticleValidationResult()
        {
            this.Issues = new List<string>();
        }

        public bool Success => !this.Issues.Any();

        public IEnumerable<string> Issues { get; set; }
    }
}