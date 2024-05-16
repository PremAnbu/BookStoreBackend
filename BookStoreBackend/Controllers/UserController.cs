using BuisinessLayer.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.DTO;
using ModelLayer.DTO.Request;
using ModelLayer.DTO.Responce;
using ModelLayer.GlobalException;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class UserController(IUserBL userService,IConfiguration _configuration) : ControllerBase
    {
        [HttpPost]
        public ResponceStructure<string> Register(UserRequest request)
        {
            try
            {
                int registrationResult = userService.Register(request);
                if (registrationResult>0)
                {
                    return new ResponceStructure<string>(true, "User registration successful");
                }
                return new ResponceStructure<string>(false, "Invalid Input");
            }
            catch (Exception ex)
            {
                // Log other exceptions
               // _logger.LogError(ex, "Error occurred during customer registration");
                return new ResponceStructure<string>(false,"An error occurred during registration",ex.Message);
            }
        }

        [HttpGet("{Email}/{password}")]
        public ResponceStructure<string> Login(String Email, String password)
        {
            string token = null;
            try
            {
                UserResponce Responce = userService.Login(Email, password);
                if (Responce != null)
                {
                    token = GenerateToken(Responce);
                }
              //  _logger.LogInformation("Success");
                return new ResponceStructure<string>(true,"Login Sucessfull",token);
            }
            catch (Exception ex)
            {
                if (ex is UserNotFoundException)
                {
                   // _logger.LogError(ex.Message);
                     return new ResponceStructure<string>(false, ex.Message);
                    //throw;
                }
                else
                {
                    //_logger.LogError(ex.Message);
                      return new ResponceStructure<string>( false, ex.Message);
                    //throw;
                }
            }
        }

        private string GenerateToken(UserResponce user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
            // Ensure the key size is at least 256 bits (32 bytes)
            if (key.Length < 32)
            {
                throw new ArgumentException("JWT secret key must be at least 256 bits (32 bytes)");
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                     new Claim(ClaimTypes.Email ,user.email),
                     new Claim(ClaimTypes.Name, user.name),
                     new Claim(ClaimTypes.MobilePhone,user.mobileNumber.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

