using KenshiWikiValidator.Features.WikiSections;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators.Rules
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

            var sectionPath = Path.Combine(sections, $"{title}-{section.Header}.txt");
            File.WriteAllText(sectionPath, sectionContent);

            if (!content.Contains(sectionContent))
            {
                result.AddIssue($"Incorrect or missing '{section.Header}' section in article '{title}'");
            }

            return result;
        }

        protected abstract WikiSectionBuilder CreateSectionBuilder(ArticleData data);
    }
}
