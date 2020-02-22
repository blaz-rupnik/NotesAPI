using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Controllers.Resources;
using NotesApp.Helpers;
using NotesApp.Models;
using NotesApp.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotesApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IUserService userService,
            ILogger<UsersController> logger
            )
        {
            this._mapper = mapper;
            this._userService = userService;
            this._appSettings = appSettings.Value;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserAuthenticateResource model)
        {
            var user = _mapper.Map<User>(model);

            try
            {
                await _userService.Create(user, model.Password);
                _logger.LogInformation($"User {model.Username} was created.");
                return Ok();
            }
            catch(DomainException ex)
            {
                _logger.LogError($"Error creating user. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticateResource model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                _logger.LogError($"Failed to authenticate user {model.Username}. Wrong or missing data.");
                return BadRequest();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            _logger.LogInformation("User successfully authenticated.");
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                Token = tokenString
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            var result = _mapper.Map<IEnumerable<UserResource>>(users);
            _logger.LogInformation("Users successfully retrieved");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
            {
                _logger.LogError($"User with id {id} not found.");
                return NotFound();
            }

            var model = _mapper.Map<UserResource>(user);
            _logger.LogInformation($"User {user.Username} successfully retrieved");
            return Ok(model);
        }
    }
}
