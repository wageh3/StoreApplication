using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Abstractions.Auth;
using Store.G02.Shard.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var result = await _serviceManager.authService.LoginAsync(request);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            var result = await _serviceManager.authService.RegistertAsync(request);
            return Ok(result);
        }
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExistAsync(string email)
        {
            var result = await _serviceManager.authService.CheckEmailExistAsync(email);
            return Ok(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.authService.GetCurrentUserAsync(email.Value);
            return Ok(result);
        }
        [HttpGet("Address")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.authService.GetCurrentUserAddressAsync(email.Value);
            return Ok(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto address)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.authService.UpdateUserAddressAsync(address, email.Value);
            return Ok(result);
        }
    }
}
