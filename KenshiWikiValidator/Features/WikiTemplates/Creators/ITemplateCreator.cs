namespace KenshiWikiValidator.Features.WikiTemplates.Creators
{
    public interface ITemplateCreator
    {
        public WikiTemplate? Generate();
    }
}
