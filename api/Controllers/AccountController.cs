using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Account;
using api.Interfaces;
using api.Modles;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
         private readonly ApplicationDBContext _context;
         private readonly UserManager<AppUser> _userManager;
         private readonly ITokenService _tokenService;
         private readonly SignInManager<AppUser> _signingManager;
        public AccountController (UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signingManager = signInManager;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user =await _userManager.Users.FirstOrDefaultAsync(a=>a.UserName.ToLower() == loginDto.Username.ToLower());

                if(user == null)
                {   
                    return Unauthorized("Invalid Username!");
                }
                var result = await _signingManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if(!result.Succeeded)
                {
                     return Unauthorized("Invalid Password!");
                }
                return  Ok(
                    new NewUserDto
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var user = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };
                

                var createUser = await _userManager.CreateAsync(user,registerDto.Password);
                if(createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user,"User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.CreateToken(user)
                        });
                    }
                    else
                    {
                        return StatusCode(500,roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500,createUser.Errors);
                }
            }
            catch(Exception ex){
                return StatusCode(500, ex.Message);
            }
        }
    }
}