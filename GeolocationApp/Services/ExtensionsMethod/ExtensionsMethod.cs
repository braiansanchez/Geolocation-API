using GeolocationApp.Data.Entities;
using GeolocationApp.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GeolocationApp.Services.ExtensionsMethod
{
    public static class ExtensionsMethod
    {
        public static IEnumerable<Location> ToModel(this IEnumerable<LocationDB> locationsDB)
        {
            return locationsDB.Select(location => location.ToModel());
        }

        public static Location ToModel(this LocationDB locationDB)
        {
            return new Location()
            {
                Id = locationDB.Id,
                Name = locationDB.Name,
                Description = locationDB.Description,
                Coordinates = locationDB.Coordinates,
                User = locationDB.User.ToModel(),
                Date = locationDB.Date,
            };
        }
        public static IEnumerable<User> ToModel(this IEnumerable<UserDB> UsersDB)
        {
            return UsersDB.Select(user => user.ToModel());
        }

        public static User ToModel(this UserDB UserDB)
        {
            return new User()
            {
                Id = UserDB.Id,
                UserName = UserDB.UserName,
                Password = UserDB.Password,
                Date = UserDB.Date
            };
        }
    }
}