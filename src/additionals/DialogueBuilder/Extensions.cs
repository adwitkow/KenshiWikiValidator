using KenshiWikiValidator.OcsProxy;

namespace DialogueDumper
{
    public static class Extensions
    {
        public static string ToCommaSeparatedListOr<T>(this IEnumerable<ItemReference<T>> input)
            where T : IItem
        {
            var names = input.Select(reference => reference.Item.Name);
            return ToCommaSeparatedList(names, "or");
        }

        public static string ToCommaSeparatedListOr(this IEnumerable<string> input)
        {
            return ToCommaSeparatedList(input, "or");
        }

        private static string ToCommaSeparatedList(IEnumerable<string> input, string lastSeparator)
        {
            if (!input.Any())
            {
                return string.Empty;
            }

            if (input.Count() == 1)
            {
                return input.Single();
            }

            var commaSeparatedElements = string.Join(", ", input.SkipLast(1));
            var lastElement = input.TakeLast(1).Single();
            return $"{commaSeparatedElements} {lastSeparator} {lastElement}";
        }
    }
}
