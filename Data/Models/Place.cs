namespace YESHome.Data.Models
{
    public class Place
    {
        public Place()
        {
            Reports = new List<Report>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
