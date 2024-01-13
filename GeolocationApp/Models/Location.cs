namespace GeolocationApp.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Coordinates { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}
