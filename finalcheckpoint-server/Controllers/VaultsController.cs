using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalcheckpoint_server.Models;
using finalcheckpoint_server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using finalcheckpoint_server.Exceptions;
using Microsoft.AspNetCore.Authorization;
using CodeWorks.Auth0Provider;

namespace finalcheckpoint_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;

        private readonly KeepsService _ks;

        public VaultsController(VaultsService vs, KeepsService ks)
        {
            _vs = vs;
            _ks = ks;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vault>> GetAll()
        {
            try
            {
                return Ok(_vs.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetById(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vs.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/Vaults
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Post([FromBody] Vault newVault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVault.CreatorId = userInfo.Id;
                Vault created = _vs.Create(newVault);
                created.Creator = userInfo;
                return Ok(created);
            }
            catch (Forbidden e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault editData)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editData.Id = id;
                editData.Creator = userInfo;
                return Ok(_vs.Edit(editData, userInfo.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/Vaults/id
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vs.Delete(id, userInfo.Id));
            }
            catch (Forbidden e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }

        }

        [HttpGet("{id}/keeps")]
        // [Authorize]
        public ActionResult<IEnumerable<Keep>> GetKeeps(int id)
        {
            try
            {
                IEnumerable<Keep> keeps = _ks.GetKeepsByVaultId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

