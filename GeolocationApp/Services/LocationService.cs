using GeolocationApp.Data;
using GeolocationApp.Data.Entities;

namespace GeolocationApp.Services
{
    public class LocationService : IGeolocationService<LocationDB>
    {
        private readonly GeolocationDBContext _dbContext;

        public LocationService(GeolocationDBContext context)
        {
            _dbContext = context;
        }

        public LocationDB? Get(int id)
        {
            return _dbContext.Locations.FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<LocationDB> GetAll()
        {
            return _dbContext.Locations.ToList();
        }

        public LocationDB Add(LocationDB location)
        {
            var locationAdded = _dbContext.Locations.Add(location).Entity;
            _dbContext.SaveChanges();
            return locationAdded;
        }

        public LocationDB Update(LocationDB location)
        {
            var locationUpdated = _dbContext.Locations.Update(location).Entity;
            _dbContext.SaveChanges();
            return locationUpdated;
        }

        public void Delete(LocationDB location)
        {
            _dbContext.Locations.Remove(location);
            _dbContext.SaveChanges();
        }
    }
}
