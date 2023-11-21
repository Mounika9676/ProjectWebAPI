using AutoMapper;
using ProjectWebAPI.Models;
using ProjectWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectWebAPI.DTO;
using ProjectWebAPI.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration, ILogger<UserController> logger)
        {
            this.userService = userService;
            _mapper = mapper;
            this.configuration = configuration;
            _logger = logger;

        }

        //Get: /GetAllUsers
        [HttpGet, Route("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> users = userService.GetAllUsers();
                List<UserDTO> usersDto = _mapper.Map<List<UserDTO>>(users);
                return StatusCode(200, users);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("Register")]
        [AllowAnonymous] //access the endpoint any any user with out login
        public IActionResult AddUser(UserDTO userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                userService.CreateUser(user);
                return StatusCode(200, user);
                // return Ok(); //return emplty result

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(500, ex.InnerException.Message);
            }
        }
        //PUT /EditUser
        [HttpPut, Route("EditUser")]
        [Authorize(Roles = "User")]
        public IActionResult EditUser(UserDTO userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                userService.EditUser(user);
                return StatusCode(200, user);
                // return Ok(); //return emplty result

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("Validate")]
        [AllowAnonymous]
        public IActionResult Validate(Login login)
        {
            try
            {
                User user = userService.ValidteUser(login.Email, login.Password);
                AuthReponse authReponse = new AuthReponse();
                if (user != null)
                {
                    authReponse.UserName = user.Name;
                    authReponse.Role = user.Role;
                    authReponse.Token = GetToken(user);
                }
                return StatusCode(200, authReponse);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        private string GetToken(User? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            //header part
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );
            //payload part
            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Email,user.Email),
                    });

            var expires = DateTime.UtcNow.AddMinutes(10);
            //signature part
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

    }
}
