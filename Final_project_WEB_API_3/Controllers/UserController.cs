using Final_project_WEB_API_3.Dto;
using Final_project_WEB_API_3.Interface;
using Final_project_WEB_API_3.Models;
using Final_project_WEB_API_3.Utilits;
using Microsoft.AspNetCore.Mvc;

namespace Final_project_WEB_API_3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly GenerateToken _generateToken;

        public UserController(IUserRepository userRepository, GenerateToken generateToken)
        {
            _userRepository = userRepository;
            _generateToken = generateToken;

        }

        [HttpPost]
        [Route("Loging")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Login([FromBody] UserLoginDto entity)
        {
            var userDatabase = _userRepository.ReadDatabase();
            var searchedUser = userDatabase.Where(x => x.Username == entity.Username && x.Password == entity.Password).FirstOrDefault();
            if (searchedUser != null)
            {
                var token = _generateToken.GenerateJwt(searchedUser);
                searchedUser.Password = "";
                return Ok(new {user = searchedUser, token = token });
            }
            return NotFound("Usuário ou senha inválidos");
        }

        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(BoardGames), StatusCodes.Status201Created)]
        public IActionResult CreatUser([FromBody] UserDto entity)
        {
            var userCreated = _userRepository.Insert(entity);
            return Created(string.Empty, userCreated);
        }
    }
}

