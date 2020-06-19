namespace FirewallWidget.Data
{
    public class Rule
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Icon { get; set; }

        public string ProgramPath { get; set; }

        public int Profile { get; set; }

        public int Direction { get; set; }

        public int Order { get; set; }
    }
}
