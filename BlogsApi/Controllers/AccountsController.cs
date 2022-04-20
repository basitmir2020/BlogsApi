using AutoMapper;
using BlogsApi.Dtos;
using BlogsApi.Helpers;
using BlogsApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BlogsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountsController> _logger;
        private IMapper _mapper;

        public AccountsController(
            IAuthManager authManager, 
            UserManager<AppUser> userManager,
            ILogger<AccountsController> logger,
            IMapper mapper)
        {
            _authManager = authManager;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("Register")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTOS userDTOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user  = _mapper.Map<AppUser>(userDTOS);
            user.UserName = userDTOS.Email;

            var result = await _userManager.CreateAsync(user,userDTOS.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Accepted();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTOS userDTOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _authManager.ValidateUser(userDTOS))
            {
                return Unauthorized();
            }
            return Accepted(new {Token = await _authManager.CreateToken()});
        }
    }
}
