using GeolocationApp.Controllers.DTO;
using GeolocationApp.Data.Entities;
using GeolocationApp.Models;
using GeolocationApp.Services;
using GeolocationApp.Services.ExtensionsMethod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GeolocationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IGeolocationService<UserDB> _userService;
        private IConfiguration _config;

        public UserController(ILogger<UserController> logger, IGeolocationService<UserDB> userService, IConfiguration config)
        {
            _userService = userService;
            _logger = logger;
            _config = config;
        }

        [HttpGet(Name = "GetUser")]
        public IEnumerable<User> GetAll()
        {
            return _userService.GetAll().ToModel();
        }

        [HttpPut(Name = "Login")]
        public string Login(UserDTO user)
        {
            var tokenString = AuthenticateUser(user);
            return tokenString;
        }

        private string AuthenticateUser(UserDTO login)
        {
            UserDB user = null;

            var users = _userService.GetAll();
            var userLoged = users.FirstOrDefault(u => u.UserName == login.UserName && u.Password == login.Password);

            if (userLoged is null)
                return string.Empty;

            var token = GenerateJSONWebToken(userLoged);
            return token;
        }

        private string GenerateJSONWebToken(UserDB userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.UserName),
                new Claim("DateOfJoing", userInfo.Date.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost(Name = "RegisterUser")]
        public User Register(UserDTO user)
        {
            var userRegistered = _userService.Add(new UserDB(user.UserName, user.Password));

            return userRegistered.ToModel();
        }

    }
}
