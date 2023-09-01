namespace YESHome.Data.Models
{
    public class Place
    {
        public Place()
        {
            Id = Guid.NewGuid().ToString();
            Reports = new List<Report>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
