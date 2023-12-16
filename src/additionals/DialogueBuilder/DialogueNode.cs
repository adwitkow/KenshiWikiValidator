namespace DialogueDumper
{
    public class DialogueNode
    {
        public DialogueNode()
        {
            this.Line = string.Empty;
            this.Children = new List<DialogueNode>();
            this.Speakers = Enumerable.Empty<string>();
            this.Conditions = Enumerable.Empty<string>();
        }

        public int Level { get; set; }
        
        public IEnumerable<string> Speakers { get; set; }

        public string Line { get; set; }

        public ICollection<DialogueNode> Children { get; set; }

        public IEnumerable<string> Conditions { get; set; }

        public IEnumerable<string> Effects { get; set; }

        public override string ToString()
        {
            string speakers;
            if (this.Speakers.Count() > 1)
            {
                speakers = this.Speakers.ToCommaSeparatedListOr();
            }
            else
            {
                speakers = this.Speakers.Single();
            }

            return $"{new string('*', this.Level)} '''{speakers}''': {this.Line}";
        }
    }
}
