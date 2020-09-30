using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Corpnet.Data.Model;
using Corpnet.Entities;
using Corpnet.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace Corpnet.API.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("MyPolicy")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly IGenericService _genService;
        private readonly IRecentLinksService _recLinks;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        IConfiguration _configuration;

        public GenericController(IGenericService genService, IRecentLinksService recentLinks, ILogger<GenericController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this._genService = genService;
            this._recLinks = recentLinks;
            this._logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;

        }

        [Authorize]
        [HttpGet("GetUsername")]
        public IActionResult GetUsername()
        {
            try
            {


                string currentUserID = "";
                //string rolename = "";
                //string result = "User not found!!";

                //string userName = "";
                //ClaimsPrincipal currentUser = this.User;
                ////currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                //userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                //userName = _httpContextAccessor.HttpContext.User.Identity.Name;
                ////return Ok(currentUserID);

                //Get username from AD
                //currentUserID = Environment.UserName;
                currentUserID = User.Identity.Name;
                //var userName = User.FindFirst("name").Value;
                return  Ok(currentUserID);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex }");
                return StatusCode(500, ex.InnerException);
            }
        }



        //[Authorize]
        //[HttpGet("GetUsernameBK")]
        //public IActionResult GetUsername_BK()
        //{
        //    try
        //    {
        //        string currentUserID = "";
        //        //string rolename = "";
        //        //string result = "User not found!!";

        //        string userName = "";
        //        ClaimsPrincipal currentUser = this.User;
        //        //currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        userName = _httpContextAccessor.HttpContext.User.Identity.Name;
        //        //return Ok(currentUserID);

        //        //Get username from AD
        //        //currentUserID = Environment.UserName;
        //        currentUserID = User.Identity.Name;
        //        //var userName = User.FindFirst("name").Value;
        //        return Ok(userName);

        //        //if (!string.IsNullOrWhiteSpace(currentUserID))
        //        //{
        //        //    IEnumerable<GenericResult> res = await _genService.GetUser(currentUserID).ConfigureAwait(false);
        //        //    if (res != null)
        //        //    {
        //        //        string[] s = res.Select(p => p.result).ToArray();
        //        //        if (s.Length > 0)
        //        //            rolename = s[0];
        //        //        else
        //        //            rolename = "user";
        //        //    }

        //        //    if (!string.IsNullOrWhiteSpace(rolename))
        //        //    {
        //        //        //create claims details based on the user information
        //        //        var claims = new[] {
        //        //         new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        //        //         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        //         new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //        //         new Claim("username", currentUserID),
        //        //         new Claim("role", rolename)
        //        //    };
        //        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //        //        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //        //        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
        //        //        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    result = "No user identity available.";
        //        //}

        //        //return Ok(result);

        //        //Not required
        //        //if (HttpContext.Request.Headers.ContainsKey("X-MS-CLIENT-PRINCIPAL-NAME"))
        //        //    currentUserID = HttpContext.Request.Headers["X-MS-CLIENT-PRINCIPAL-NAME"][0];

        //        //string userName = "";
        //        //ClaimsPrincipal currentUser = this.User;
        //        //var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        //userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        //userName = _httpContextAccessor.HttpContext.User.Identity.Name;
        //        // return Ok(currentUserID);

        //        //if (!string.IsNullOrWhiteSpace(rolename))
        //        //{
        //        //     generate token that is valid for 7 days
        //        //    var tokenHandler = new JwtSecurityTokenHandler();
        //        //    var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
        //        //    var tokenDescriptor = new SecurityTokenDescriptor
        //        //    {
        //        //        Subject = new ClaimsIdentity(new[] { new Claim("username", currentUserID.ToString()), new Claim("role", rolename) }),
        //        //        Expires = DateTime.UtcNow.AddDays(7),
        //        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //        //    };
        //        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //        //    return Ok(tokenHandler.WriteToken(token));
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong: {ex }");
        //        return StatusCode(500, ex.InnerException);
        //    }
        //}

        [HttpGet("page/{id}")]
        public async Task<IActionResult> GetGenericPage(int id)
        {
            try
            {
                string dirJson = await _genService.GetGenericPage(id).ConfigureAwait(false);
                //_logger.LogInformation("Test Log");
                return Ok(dirJson);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex }");
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpGet("GetGenericPage")]
        public async Task<IActionResult> GetGenericPageAll(int id)
        {
            try
            {
                string dirJson = await _genService.GetGenericPageAll(id).ConfigureAwait(false);
                return Ok(dirJson);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex }");
                return StatusCode(500, ex.InnerException);
            }

        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddGenericPage(GenericPageDto gen, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _genService.AddGenericPage(gen, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest();
                else
                    return Ok("Page Added Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex }");
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateGenericPage(int id, GenericPageDto gen, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _genService.UpdateGenericPage(id, gen, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest();
                else
                    return Ok("Page Updated Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex }");
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPut("Delete")]
        public async Task<IActionResult> DeleteGenericPage(int id, string username, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _genService.DeleteGenericPage(id, username, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest();
                else
                    return Ok("Page Deleted Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex }");
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpGet("GetRecentLinks")]
        public async Task<IActionResult> GetRecentLinks(string username)
        // public IActionResult GetRecentLinks(string username)
        {
            try
            {
                //string dirJson = _recLinks.GetRecentLinks(username);
                string dirJson = await _recLinks.SPGetRecentLinks(username).ConfigureAwait(false);

                return Ok(dirJson);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex }");
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPost("AddRecentLink")]
        public async Task<IActionResult> AddRecentLink(RecentLinksDto recentDto, CancellationToken cancellationToken)
        {
            try
            {
                int result = await _recLinks.AddRecentLink(recentDto, cancellationToken).ConfigureAwait(false);
                if (result == 0)
                    return BadRequest();
                else
                    return Ok("Added to Recent Link");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex }");
                return StatusCode(500, ex.InnerException);
            }
        }

    }
}
