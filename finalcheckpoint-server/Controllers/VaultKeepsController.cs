using System;
using Microsoft.AspNetCore.Mvc;
using finalcheckpoint_server.Models;
using finalcheckpoint_server.Services;
using CodeWorks.Auth0Provider;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using finalcheckpoint_server.Exceptions;

namespace finalcheckpoint_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _service;

        private readonly VaultsService _vs;

        public VaultKeepsController(VaultKeepsService service, VaultsService vs)
        {
            _service = service;
            _vs = vs;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vault>> GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        [HttpPost]

        public async Task<ActionResult<VaultKeep>> Post([FromBody] VaultKeep newVau)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVau.CreatorId = userInfo.Id;
                VaultKeep created = _service.Create(newVau);
                return Ok(created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            try
            {
                return Ok(_service.Delete(id, userInfo.Id));
            }
            catch (Forbidden e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
        }


    }
}