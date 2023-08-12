namespace YesHome.Data.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public DateTime WorkStart { get; set; }
        public Place Place { get; set; } = new Place();
        public User User { get; set; } = new User();

    }
}
