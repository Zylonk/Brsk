namespace ClientWeb.Models
{
    public class Tasks
    {
        public string Id { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Timeframe { get; set; }

        public string Priority { get; set; } = null!;

        public bool Done { get; set; }
    }
}
