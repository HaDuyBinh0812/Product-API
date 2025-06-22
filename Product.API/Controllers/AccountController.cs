using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Product.API.Errors;
using Product.API.Extensions;
using Product.Core.Dto;
using Product.Core.Entities;
using Product.Core.Interface;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenServices _tokenServices;
        public AccountController(UserManager<AppUser> userManager, ITokenServices tokenServices, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenServices = tokenServices;
        }

        [HttpPost("Login")]
        public async Task <IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null) return Unauthorized(new BaseCommonResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (result is null || result.Succeeded == false) return Unauthorized(new BaseCommonResponse(401));
            return Ok(new UserDto
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = _tokenServices.CreateToken(user),
                });
        }
        [HttpGet("check-email-exist")]
        public async Task<ActionResult<bool>> CheckEmailExist([FromQuery] string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if(result is not null)
            {
                return true;
            }
            return false;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (CheckEmailExist(dto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = new[] { "This email is already Token" }
                });
            }
            var user = new AppUser
            {
                DisplayName = dto.DisplayName,
                UserName= dto.Email,
                Email = dto.Email,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded == false) return BadRequest(new BaseCommonResponse(400));
            return Ok(new UserDto
            {
                DisplayName= dto.Email,
                Email = dto.Email,
                Token = _tokenServices.CreateToken(user)
            });
        }

        [Authorize]
        [HttpGet("Test")]
        public ActionResult <string> Test()
        {
            return "hi";
        }

        [Authorize]
        [HttpGet("get-current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FidndEmailByClaimPrincipal(HttpContext.User);
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenServices.CreateToken(user)
            });
        }

        [Authorize]
        [HttpGet("get-user-address")]
        public async Task<IActionResult> GetUserAddress()
        {
            var user = await _userManager.FidndUserByClaimPrincipalWithAddress(HttpContext.User);
            var _result = _mapper.Map<Address, AddressDto>(user.Address); 
            return Ok(_result);
        }
        [Authorize]
        [HttpPut("update-user-address")]
        public async Task<IActionResult> UpdateUserAddress(AddressDto dto)
        {
            var user = await _userManager.FidndUserByClaimPrincipalWithAddress(HttpContext.User);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            return BadRequest($"problem in update this {HttpContext.User}");
        }
    }
}
