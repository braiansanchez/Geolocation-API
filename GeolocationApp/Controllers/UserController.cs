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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IGeolocationService<UserDB> _userService;

        public UserController(ILogger<UserController> logger, IGeolocationService<UserDB> userService)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet(Name = "GetUser")]
        public IEnumerable<User> GetAll()
        {
            return _userService.GetAll().ToModel();
        }

        [HttpPost(Name = "RegisterUser")]
        public User Register(UserDTO user)
        {
            var userRegistered = _userService.Add(new UserDB(user.UserName, user.Password));

            return userRegistered.ToModel();
        }

        [HttpPut("{id:int}")]
        public User Update(UserDTO user, int id)
        {
            var userToUpdate = _userService.Get(id);

            if (userToUpdate is null)
                return null;

            userToUpdate.UpdateValues(user.UserName, user.Password);
            var userUpdated = _userService.Update(userToUpdate);

            return userUpdated.ToModel();
        }
    }
}
