using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Test.Data;
using Test.Exceptions;
using Test.Model;

namespace Test.System.Users
{
    public class UserService : IUserService
    {
        private readonly SignInManager<AppUser> _singInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager,SignInManager<AppUser> singInManager, RoleManager<AppRole> roleManager,IConfiguration config)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _roleManager= roleManager;
            _config = config;
        }
        public async Task<string> Authencate(LoginRequest request)
        {
            var user =await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;
            

            var result =await _singInManager.PasswordSignInAsync(user,request.Password,request.RememberMe,true);
            if (!result.Succeeded)
            {
                return null;
            }

            var role = _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.GivenName, user.LastName),
                new Claim(ClaimTypes.GivenName, user.PhoneNumber),
                new Claim("Password",user.PasswordHash),
                new Claim("Dob",user.Dob.ToString()),
                new Claim(ClaimTypes.Role, string.Join(";", role))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
            _config["Tokens:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials:creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                UserName = request.User,
                PhoneNumber = request.PhoneNumber,
                LastName = request.LastName
                

            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
