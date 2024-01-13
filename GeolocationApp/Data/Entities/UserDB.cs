using Microsoft.AspNetCore.Identity;

namespace GeolocationApp.Data.Entities
{
    public class UserDB 
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; }

        public UserDB(string username, string password)
        {
            UserName = username;
            Password = password;
            Date = DateTime.UtcNow;
        }

        public UserDB()
        {
        }

        public void UpdateValues(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
