using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Domain.Helpers;
using TaskManager.Domain.Models;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach(var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey = _configuration["JWT:Secret"];
            var issuerConf = _configuration["JWT:ValidIssuer"];
            var audienceConf = _configuration["JWT:ValidAudience"];

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var token = new JwtSecurityToken(
                    issuer: issuerConf,
                    audience: audienceConf,
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
        if(model.Usertype != UserRoles.Developer && model.Usertype != UserRoles.Manager)
            return StatusCode(StatusCodes.Status400BadRequest, new {Status = "Error", Message = "Invalid User Type."});

        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = UserMessages.USER_ALREADY_EXISTS });

        IdentityUser user = new IdentityUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status400BadRequest, new { Status = "Error", Message = UserMessages.USER_CREATION_UNSUCCESSFUL });

        if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
        if (!await _roleManager.RoleExistsAsync(UserRoles.Developer))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Developer));

        if (await _roleManager.RoleExistsAsync(UserRoles.Manager))
        {
            if(model.Usertype == UserRoles.Manager)
                await _userManager.AddToRoleAsync(user, UserRoles.Manager);
        }

        if(await _roleManager.RoleExistsAsync(UserRoles.Developer))
        {
            if (model.Usertype == UserRoles.Developer)
                await _userManager.AddToRoleAsync(user, UserRoles.Developer);
        }

        return Ok(new { Status = "Success", Message = UserMessages.USER_CREATED_SUCCESSFULY + $" {model.Username}" });
    }
}