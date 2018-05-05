using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AliasManager.Auth;
using AliasManager.Models;
using AliasManager.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AliasManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]User credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userName));
        }
    }
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}