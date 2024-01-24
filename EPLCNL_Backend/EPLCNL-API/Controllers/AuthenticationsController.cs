using AutoMapper;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.AccountsService;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using ViewModel;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace EPLCNL_API.Controllers
{
    [Route("api/authentications")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;


       

        public AuthenticationsController(IAccountService accountService, IConfiguration configuration,
            IOptionsMonitor<AppSettings> optionsMonitor)
        {
                _accountService = accountService;
            _configuration = configuration;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpPost("login")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<AccountResponse>> LoginMember(LoginMem logMem)
        {
            AccountResponse memberModel = await _accountService.Login(logMem);

            if (memberModel == null)
            {
                if (logMem.Email!.Equals(_configuration["Admin:Email"], StringComparison.OrdinalIgnoreCase)
                    && logMem.Password!.Equals(_configuration["Admin:Password"]))
                {
                    var adminModel = new AccountResponse
                    {
                        Email = _configuration["Admin:Email"],
                        Password = _configuration["Admin:Password"]
                    };
                    var token = GenerateToken(adminModel);

                    var response = new ApiResponse
                    {
                        Success = true,
                        Message = "Authentication Successful",
                        Data = token
                    };
                    return Ok(response);

                }

                return NotFound(
                        new ApiResponse
                        {
                            Success = false,
                            Message = "Invalid Username or Password"
                        }
                 );
            }

            else
            {
                if (memberModel.IsActive == true)
                {
                    //cap token neu hop le
                    return Ok(
                              new ApiResponse
                              {
                                  Success = true,
                                  Message = "Authenticate Successfully",
                                  Data = GenerateToken(memberModel)
                              }
                        );
                }
                else
                {
                    return NotFound();
                }

            }
        }
        //ham cap token
        private string GenerateToken(AccountResponse model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", model.Id.ToString()),
                    new Claim(ClaimTypes.Role, model.RoleId.ToString()),

                    //roles
                    new Claim("Token Id", Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1440),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes)
                        , SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
