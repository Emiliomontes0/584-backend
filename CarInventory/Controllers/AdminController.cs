using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using modelDB;
using CarInventory.Dtos;
using CarInventory.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace CarInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<SourceContextUser> userManager, JwtHandler jwtHandler) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(LoginRequest loginRequest)
        {
            SourceContextUser? user = await userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
            {
                return Unauthorized("Error: username not found");
            }

            bool success = await userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!success)
            {
                return Unauthorized("Error: incorrect password");
            }

            JwtSecurityToken token = await jwtHandler.GetTokenAsync(user);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginResult
            {
                Success = true,
                Message = "Login successful",
                Token = tokenString
            });
        }
    }
}
