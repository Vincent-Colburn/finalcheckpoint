using System;
using Microsoft.AspNetCore.Mvc;
using finalcheckpoint_server.Models;
using finalcheckpoint_server.Services;
using CodeWorks.Auth0Provider;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public ActionResult<string> Delete(int id)
        {
            try
            {
                return Ok(_service.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}