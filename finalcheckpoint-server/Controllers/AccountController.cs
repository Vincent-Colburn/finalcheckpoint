using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using finalcheckpoint_server.Models;
using finalcheckpoint_server.Services;

namespace finalcheckpoint_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ProfilesService _ps;

        private readonly VaultsService _vs;

        public AccountController(ProfilesService ps, VaultsService vs)
        {
            _ps = ps;
            _vs = vs;
        }

        [HttpGet]
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
                // REVIEW[epic=Authentication] how to get the user info from the request token
                // same as to req.userInfo
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}