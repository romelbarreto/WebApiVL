using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiVL.Models;

namespace WebApiVL.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UsuarioController(UserManager<Usuario> userManager,
            IConfiguration configuration, RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleDTO roleDTO)
        {
            var response = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleDTO.RoleName
            });

            if (response.Succeeded)
            {
                return Ok("Nuevo Rol Creado");
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(AsignarRolUsuarioDTO assignRoleToUserDTO)
        {

            var userDetails = await _userManager.FindByEmailAsync(assignRoleToUserDTO.Correo);

            if (userDetails != null)
            {

                var userRoleAssignResponse = await _userManager.AddToRoleAsync(userDetails, assignRoleToUserDTO.Acceso);

                if (userRoleAssignResponse.Succeeded)
                {
                    return Ok("Acceso asignado a Usuario: " + assignRoleToUserDTO.Acceso);
                }
                else
                {
                    return BadRequest(userRoleAssignResponse.Errors);
                }
            }
            else
            {
                return BadRequest("No hay usuarios registrados con este Correo");
            }


        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            var response = new MainResponse();
            if (refreshTokenRequest is null)
            {
                response.ErrorMessage = "Invalid  request";
                return BadRequest(response);
            }

            var principal = GetPrincipalFromExpiredToken(refreshTokenRequest.AccessToken);

            if (principal != null)
            {
                var email = principal.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Email);

                var user = await _userManager.FindByEmailAsync(email?.Value);

                if (user is null || user.RefreshToken != refreshTokenRequest.RefreshToken)
                {
                    response.ErrorMessage = "Invalid Request";
                    return BadRequest(response);
                }

                string newAccessToken = GenerateAccessToken(user);
                string refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                await _userManager.UpdateAsync(user);

                response.IsSuccess = true;
                response.Content = new AuthenticationResponse
                {
                    RefreshToken = refreshToken,
                    AccessToken = newAccessToken
                };
                return Ok(response);
            }
            else
            {
                return ErrorResponse.ReturnErrorResponse("Invalid Token Found");
            }

        }
        private string GenerateAccessToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, $"{user.Nombre}"),
                    new Claim(ClaimTypes.Email, user.Correo),
                    

            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["JWT:Audience"],
                Issuer = _configuration["JWT:Issuer"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetail), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JWT:Issuer"],
                ValidAudience = _configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(keyDetail),
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameter, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }



        private string GenerateRefreshToken()
        {

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
