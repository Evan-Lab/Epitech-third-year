namespace AreaServer.Core.Models
{
    public class UserArea
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public DateTime DateCreation { get; set; }
        public AreaStatus StatusArea { get; set; } = AreaStatus.Pending;
        public string StatusInfo { get; set; } = string.Empty;
        public int IsActive { get; set; } = 1;

        public User User { get; set; }

        public ActionArea ActionArea { get; set; }

        public string ParamAction { get; set; } = string.Empty;

        public string InfoAction {  get; set; } = string.Empty;

        public ReactionArea ReactionArea { get; set; }

        public string ParamReaction {  get; set; } = string.Empty;

        public string InfoReaction { get; set; } = string.Empty;
    }
}
