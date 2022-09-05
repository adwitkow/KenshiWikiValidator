namespace DialogueDumper
{
    public class NodeLevelCalculator
    {
        public void CalculateLevels(int level, ICollection<DialogueNode> nodes)
        {
            foreach (var node in nodes)
            {
                node.Level = level;

                var nextLevel = CalculateNextLevel(level, nodes.Count, node.Children.Count, false);
                CalculateLevels(nextLevel, node.Children);
            }
        }

        private static int CalculateNextLevel(int level, int dialogueLinesCount, int nextLinesCount, bool isInterjection)
        {
            int nextLevel;

            if ((dialogueLinesCount > 1 || nextLinesCount > 1) && !isInterjection)
            {
                nextLevel = level + 1;
            }
            else
            {
                nextLevel = level;
            }

            return nextLevel;
        }
    }
}
