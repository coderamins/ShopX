using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopX.Identity.Application.DTOs;
using ShopX.Identity.Application.Interfaces;
using ShopX.Identity.Domain.Entities;

namespace ShopX.Identity.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email, FullName = dto.FullName };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("ثبت‌نام با موفقیت انجام شد");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Unauthorized("کاربر یافت نشد");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) return Unauthorized("رمز عبور اشتباه است");

            var token = _tokenService.CreateToken(user);
            return Ok(new AuthResponseDto(token, user.Email!, user.FullName));
        }
    }
}
