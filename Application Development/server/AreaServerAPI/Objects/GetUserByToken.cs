using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AreaServerAPI.Objects
{
    public class DecryptTokenUser
    {
        private HttpRequest _request;
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;

        public DecryptTokenUser(HttpRequest request, IConfiguration configuration, IRepository<User> userRepository)
        {
            _request = request;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<User> GetInfoUserWithToken()
        {
            if (_request.Headers.ContainsKey("Authorization"))
            {
                var authHeader = _request.Headers["Authorization"].ToString();

                if (authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring("Bearer ".Length);
                    var key = Encoding.UTF8.GetBytes(_configuration["AppSettings:SecretKey"]);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    SecurityToken validatedToken;
                    var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                    if (validatedToken is JwtSecurityToken jwtSecurityToken)
                    {
                        var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.Name);
                        if (userIdClaim != null)
                        {
                            Console.WriteLine("User ID: " + userIdClaim.Value);
                            var foundUser = await _userRepository.GetAsync(p => p.Id == int.Parse(userIdClaim.Value));
                            return foundUser;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid token");
                    }
                }
                else
                {
                    Console.WriteLine("No Bearer token found in the Authorization header.");
                }
            }
            else
            {
                Console.WriteLine("No Authorization header found in the request.");
            }
            return null;
        }
    }
}
