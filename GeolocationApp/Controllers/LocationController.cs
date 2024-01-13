using GeolocationApp.Controllers.DTO;
using GeolocationApp.Data.Entities;
using GeolocationApp.Models;
using GeolocationApp.Services;
using GeolocationApp.Services.ExtensionsMethod;
using Microsoft.AspNetCore.Mvc;

namespace GeolocationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly IGeolocationService<LocationDB> _locationService;

        public LocationController(ILogger<LocationController> logger, IGeolocationService<LocationDB> locationService)
        {
            _locationService = locationService;
            _logger = logger;
        }

        [HttpGet(Name = "GetLocations")]
        public IEnumerable<Location> GetAll()
        {
            return _locationService.GetAll().ToModel();
        }

        [HttpPost(Name = "AddLocations")]
        public Location Add(LocationDTO location)
        {
            var locationAdded = _locationService.Add(new LocationDB(location.Name, location.Description, location.Coordinates));

            return locationAdded.ToModel();
        }

        [HttpPut("{id:int}")]
        public Location Update(LocationDTO location, int id)
        {
            var locationToUpdate = _locationService.Get(id);

            if (locationToUpdate is null)
                return null;

            locationToUpdate.UpdateValues(location.Name, location.Description, location.Coordinates);
            var locationUpdated = _locationService.Update(locationToUpdate);
            
            return locationUpdated.ToModel();
        }

        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            var locationToDelete = _locationService.Get(id);

            if (locationToDelete is null)
                return;

            _locationService.Delete(locationToDelete);
        }
    }
}
