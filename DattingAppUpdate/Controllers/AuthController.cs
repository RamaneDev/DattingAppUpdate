﻿using DattingAppUpdate.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using DattingAppUpdate.Entites;
using DattingAppUpdate.Dtos;
using DattingAppUpdate.Errors;
using DattingAppUpdate.Extensions;
using System.Linq;
using AutoMapper;

namespace DattingAppUpdate.Controllers
{ 
    public class AuthController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserToLoginDto model)
        {
            var user = await _userManager.GetUserByNameWithPhotos(model.Username);
          
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user); 

                var token = GetToken(userRoles, user);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    username = user.UserName,
                    photoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url
                   
                });
            }
            return Unauthorized(new ApiErrorResponse(401));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserToRegisterDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status500InternalServerError, "User already exists!"));

            var user = _mapper.Map<User>(model);          

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status500InternalServerError, "User creation failed! Please check user details and try again."));

            var userRoles = await _userManager.GetRolesAsync(user);

            var token = GetToken(userRoles, user);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                username = user.UserName             

            }); ;           
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserToRegisterDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status500InternalServerError, "User already exists!"));        

            User user = new()
            {
                Email = model.Username + "@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status500InternalServerError, "User creation failed! Please check user details and try again."));

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole<int>(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        private JwtSecurityToken GetToken(IList<string> userRoles, User user)
        {

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
   
    }
}
