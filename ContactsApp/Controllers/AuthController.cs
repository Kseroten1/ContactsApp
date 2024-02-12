//using ContactsApp.DTO;
//using ContactsApp.Repositories;
//using Microsoft.AspNetCore.Authentication.BearerToken;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace ContactsApp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        public static User user = new User();
//        private readonly IConfiguration _configuration;
//        private readonly IUserRepository _userRepository;
//        public AuthController(IConfiguration configuration, IUserRepository userRepository) 
//        {
//            _configuration = configuration;
//            _userRepository = userRepository;
//        }

//        [HttpPost("register")]
//        public async Task<ActionResult<User>> Register(UserDto request)
//        {
//            user.Username = request.Username;
//            user.PasswordHash = passwordHash;
//            await _userRepository.AddAsync(user);
//            return Ok(user);
//        }



//        [HttpPost("login")]
//        public ActionResult<User> Login(UserDto request)
//        {
//            var claimsPrincipal = new ClaimsPrincipal(
//              new ClaimsIdentity(
//                new[] { new Claim(ClaimTypes.Name, request.Username) },
//                BearerTokenDefaults.AuthenticationScheme  
//              )
//            );

//            return SignIn(claimsPrincipal);
//        }
//    }
//}