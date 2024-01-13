using GeolocationApp.Controllers.DTO;
using GeolocationApp.Data.Entities;
using GeolocationApp.Models;
using GeolocationApp.Services;
using GeolocationApp.Services.ExtensionsMethod;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        public IEnumerable<Location> GetAll()
        {
            var locations = _locationService.GetAll().ToModel();

            //Populate locations if empty
            if (!locations.Any())
                locations = SeedData();

            return locations;
        }

        [HttpPost(Name = "AddLocations")]
        [Authorize]
        public Location Add(LocationDTO location)
        {
            var user = User.FindFirst(ClaimTypes.Email)?.Value;
            var locationAdded = _locationService.Add(new LocationDB(location.Name, location.Description, location.Coordinates, user));

            return locationAdded.ToModel();
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public Location Update(LocationDTO location, int id)
        {
            var locationToUpdate = _locationService.Get(id);

            if (locationToUpdate is null)
                return null;


            var user = User.FindFirst(ClaimTypes.Email)?.Value;
            locationToUpdate.UpdateValues(location.Name, location.Description, location.Coordinates, user);
            var locationUpdated = _locationService.Update(locationToUpdate);
            
            return locationUpdated.ToModel();
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public void Delete(int id)
        {
            var locationToDelete = _locationService.Get(id);

            if (locationToDelete is null)
                return;

            _locationService.Delete(locationToDelete);
        }



        private IEnumerable<Location> SeedData()
        {
            var locations = new List<LocationDTO>()
            {
                new LocationDTO() { Name = "Cordoba", Description = "Argentina", Coordinates = "-31.42220254271088, -64.1969675428217" },
                new LocationDTO() { Name = "Buenos Aires", Description = "Argentina", Coordinates = "-34.60658371725723, -58.393878749469046" },
                new LocationDTO() { Name = "Montevideo", Description = "Uruguay", Coordinates = "-34.91038930701896, -56.18031363931213" },
                new LocationDTO() { Name = "Sao Paulo", Description = "Brasil", Coordinates = "-24.033729476149055, -46.36229352606404" },
            };

            var locationsAdded = new List<Location>();

            foreach (var location in locations)
            {
                locationsAdded.Add(Add(location));
            }

            return locationsAdded;
        }
    }
}
