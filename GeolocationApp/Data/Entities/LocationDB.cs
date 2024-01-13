namespace GeolocationApp.Data.Entities
{
    public class LocationDB
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Coordinates { get; set; }
        public UserDB User { get; set; }
        public DateTime Date { get; set; }

        public LocationDB(string name, string description, string coordinates)
        {
            Name = name;
            Description = description;
            Coordinates = coordinates;
            User = new UserDB();
            Date = DateTime.UtcNow;
        }

        public void UpdateValues(string name, string description, string coordinates)
        {
            Name = name;
            Description = description;
            Coordinates = coordinates;
            Date = DateTime.UtcNow;
        }
    }
}
