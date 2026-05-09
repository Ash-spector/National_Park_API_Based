using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using National_Park_API.Models;
using National_Park_API.Repository.IRepository;

namespace National_Park_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] User user)
        {
            if(ModelState.IsValid)
            {
                var isUniqueUser = _userRepository.IsUniqueUser(user.Username);
                if(!isUniqueUser) return BadRequest("Username is already taken");
                var UserInfo = _userRepository.Register(user.Username, user.Password);
                if(UserInfo == null) return BadRequest("Wrong user / password");
                user = UserInfo;
            }
            return Ok(user);
        }

    }
}
