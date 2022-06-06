using System.Security.Claims;
using API.DTOs;
using API.Services;
using Application.Handlers.Accounts;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<PlayerDb> _userManager;
        private readonly SignInManager<PlayerDb> _signInManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<PlayerDb> userManager, SignInManager<PlayerDb> signInManager, 
            TokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<PlayerDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                return CreatePlayerObject(user);
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult<PlayerDto>> Register(RegisterDto registerDto)
        {
            if(await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("Email taken");
            }
            if(await _userManager.Users.AnyAsync(x => x.Name == registerDto.Name))
            {
                return BadRequest("Name taken");
            }
            if(await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                return BadRequest("User name taken");
            }

            var user = new PlayerDb
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                IsReady = false,
                IsGoing = false
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(result.Succeeded)
            {
                return CreatePlayerObject(user);
            }

            return BadRequest("Problem register user");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PlayerDto>> GetCurrentUser()
        {

            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            return CreatePlayerObject(user);
        }

        [HttpGet("changeStatusPrepare/{name}")]
        public async Task<IActionResult> ChangeStatusToReady(string name)
        {
            return HandleResult(await Mediator.Send(new ReadyToGame.Command { Name = name }));
        }

        [HttpGet("checkStatusPrepare/{name}")]
        public async Task<IActionResult> CheckStatus(string name)
        {
            return HandleResult(await Mediator.Send(new CheckStatusPrepare.Command { Name = name }));
        }

        [HttpGet("changeStatusGoing/{name}")]
        public async Task<IActionResult> ChangeStatusGoing(string name)
        {
            return HandleResult(await Mediator.Send(new ReadyToGoing.Command { Name = name }));
        }

        [HttpGet("checkStatusGoing/{name}")]
        public async Task<IActionResult> CheckStatusGoing(string name)
        {
            return HandleResult(await Mediator.Send(new CheckStatusGoing.Command { Name = name }));
        }

        private PlayerDto CreatePlayerObject(PlayerDb player)
        {
            return new PlayerDto
            {
                Name = player.Name,
                Token = _tokenService.CreateToken(player),
            };
        }
    }
}