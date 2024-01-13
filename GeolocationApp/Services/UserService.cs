using GeolocationApp.Data;
using GeolocationApp.Data.Entities;

namespace GeolocationApp.Services
{
    public class UserService : IGeolocationService<UserDB>
    {
        private readonly GeolocationDBContext _dbContext;

        public UserService(GeolocationDBContext context)
        {
            _dbContext = context;
        }

        public UserDB? Get(int id)
        {
            return _dbContext.Users.FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<UserDB> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public UserDB Add(UserDB user)
        {
            var userAdded = _dbContext.Users.Add(user).Entity;
            _dbContext.SaveChanges();
            return userAdded;
        }

        public UserDB Update(UserDB user)
        {
            var userUpdated = _dbContext.Users.Update(user).Entity;
            _dbContext.SaveChanges();
            return userUpdated;
        }

        public void Delete(UserDB location)
        {
        }
    }
}
