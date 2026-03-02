using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Domain.Exceptions.BadRequest;
using Store.G02.Domain.Exceptions.NotFound.Auth;
using Store.G02.Domain.Exceptions.Unauthorized;
using Store.G02.Services.Abstractions.Auth;
using Store.G02.Shard;
using Store.G02.Shard.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Auth
{
    public class AuthService(UserManager<AppUser> _userManager, IOptions<JwtOptions> _options) : IAuthService
    {
        public async Task<UserResponce> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundException(request.Email);

            var flag = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!flag) throw new UserUnauthorizedException();

            return new UserResponce
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await CreateTokenAsync(user)
            };
        }

        public async Task<UserResponce> RegistertAsync(RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword) throw new ConfirmPassBadRequestException();
            var user = new AppUser
            {
                DisplayName = request.DisplayName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user,request.Password);
            if(!result.Succeeded) throw new RegisterationBadRequestException(result.Errors.Select(e=>e.Description).ToList());

            return new UserResponce
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await CreateTokenAsync(user)
            };

        }
        
        private async Task<string> CreateTokenAsync(AppUser user)
        {

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var jwtOptions = _options.Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey));
            var token = new JwtSecurityToken(
                    issuer: jwtOptions.Issuer,
                    audience: jwtOptions.Audience,
                    claims: authClaims,
                    expires: DateTime.Now.AddDays(jwtOptions.DurationInDays),
                    signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
