﻿using System.Text.RegularExpressions;
using KenshiWikiValidator.Features.WikiSections;

namespace KenshiWikiValidator.Features.ArticleValidation.Shared
{
    public abstract class ContainsSectionRuleBase : IValidationRule
    {
        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();

            var sectionBuilder = this.CreateSectionBuilder(data);

            if (sectionBuilder is null)
            {
                return result;
            }

            var section = sectionBuilder.WikiSection;
            var sectionContent = sectionBuilder.Build();

            var sections = "sections";
            if (!Directory.Exists(sections))
            {
                Directory.CreateDirectory(sections);
            }

            var contentToValidate = MakeNewlinesConsistent(content);

            if (!contentToValidate.Contains(sectionContent))
            {
                result.AddIssue($"Incorrect or missing '{section.Header}' section");

                var sectionPath = Path.Combine(sections, $"{title}-{section.Header}.txt");
                File.WriteAllText(sectionPath, sectionContent);
            }

            return result;
        }

        protected abstract WikiSectionBuilder CreateSectionBuilder(ArticleData data);

        private static string MakeNewlinesConsistent(string input)
        {
            return Regex.Replace(input, @"\r\n|\r|\n", Environment.NewLine);
        }
    }
}
