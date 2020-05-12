using System.Drawing;

namespace FirewallWidget.Manager.DTO
{
    public class RuleDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Bitmap Icon { get; set; }

        public string ProgramPath { get; set; }

        public ProfileDto Profile { get; set; }

        public RuleDirectionDto Direction { get; set; }
    }
}
