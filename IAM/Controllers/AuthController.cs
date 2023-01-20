using System;
using Microsoft.AspNetCore.Mvc;
using IAM.Models;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using IAM.Services;
using IAM.Models.DTO;

namespace IAM.Controllers
{
	[ApiController]
	[Route("api/auth")]
	public class AuthController : ControllerBase
	{
		private IConfiguration _configuration;
		private IAuthService _authService;

		public AuthController(IConfiguration configuration, IAuthService authService)
		{
			_configuration = configuration;
			_authService = authService;
		}


		[HttpPost("login")]
        [AllowAnonymous]
        public IResult Login([FromBody] string requestBody)
		{
			string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            if (requestBody == null)
			{
                return Results.NoContent();
            }

			Req_LoginDTO? user = JsonSerializer.Deserialize<Req_LoginDTO>(requestBody);

            if (user == null || user.Username == null || user.Username.Length == 0)
            {
                Console.WriteLine("No parameters provided");
                return Results.NoContent();
            }

            var issuer = "http://localhost:5000";
			var audience = "http://localhost:4200";
			var key = Encoding.ASCII.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr");

            Tuple<Res_LoginDTO, int> loginInfoTuple = _authService.GetLoginInfo(user.Username, ipAddress);


            Res_LoginDTO loginInfo = loginInfoTuple.Item1;
			int returnValue = loginInfoTuple.Item2;

			if(returnValue != 0)
			{
                Console.WriteLine("Issue in SP, returnValue - ", returnValue);
                return Results.Unauthorized();
            }

            Console.WriteLine("Provided username - " + user.Username);
            Console.WriteLine("Provided pw - " + user.Password);

            Console.WriteLine("UserId - " + loginInfo.Id.ToString());
            Console.WriteLine("Username - " + loginInfo.Username);
            Console.WriteLine("Password - " + loginInfo.Password);
            Console.WriteLine("Email - " + loginInfo.Email);

            if (loginInfo.Password == user.Password)
			{
				var claims = new[]
				{
					new Claim("Id", Guid.NewGuid().ToString()),
					new Claim("UserId", loginInfo.Id.ToString()),
					new Claim(JwtRegisteredClaimNames.Sub, loginInfo.Username),
					new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
				};

				var signIn = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken
				(
					issuer,
					audience,
					claims,
					expires: DateTime.UtcNow.AddMinutes(60),
					signingCredentials: signIn
				);

				return Results.Ok(new JwtSecurityTokenHandler().WriteToken(token));
			}
			return Results.Unauthorized();
		}
	}

}

