namespace AreaServer.Core.Models
{
    public class ReactionArea
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DateCreation { get; set; }

        public Services Service { get; set; }
    }
}
