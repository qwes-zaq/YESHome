namespace YESHome.Data.Models
{
    public class Report
    {
        public Report()
        { 
            Id=Guid.NewGuid().ToString();
            WorkEnd = DateTime.MinValue;
        }
        public string Id { get; set; }
        public string? UserId { get; set; }
        public string PlaceId { get; set; }
        public DateTime WorkStart { get; set; }
        public DateTime? WorkEnd { get; set; }
        public Place Place { get; set; } = new Place();
        public User User { get; set; } = new User();

    }
}
