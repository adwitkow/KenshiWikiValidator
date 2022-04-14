namespace DialogueDumper
{
    public class DialogueNode
    {
        public DialogueNode()
        {
            this.Children = new List<DialogueNode>();
        }

        public int Level { get; set; }
        
        public IEnumerable<string> Speakers { get; set; }

        public string Line { get; set; }

        public ICollection<DialogueNode> Children { get; set; }

        public IEnumerable<string> Conditions { get; set; }

        public override string ToString()
        {
            string speakers;
            if (this.Speakers.Count() > 1)
            {
                speakers = string.Join(", ", this.Speakers.SkipLast(1)) + " or " + this.Speakers.TakeLast(1).Single();
            }
            else
            {
                speakers = this.Speakers.Single();
            }

            return $"{new string('*', this.Level)} '''{speakers}''': {this.Line}";
        }
    }
}
