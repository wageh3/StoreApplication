using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Abstractions.Auth;
using Store.G02.Shard.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
