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

        private readonly KeepsService _ks;

        public VaultKeepsController(VaultKeepsService service, VaultsService vs, KeepsService ks)
        {
            _service = service;
            _vs = vs;
            _ks = ks;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<VaultKeep>> GetById(int id)
        {
            try
            {
                return Ok(_vs.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
        [Authorize]

        public async Task<ActionResult<VaultKeep>> Post([FromBody] VaultKeep newVau)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVau.CreatorId = userInfo.Id;
                VaultKeep created = _service.Create(newVau);
                _ks.IncreaseKeeps(newVau.KeepId, newVau.CreatorId);
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
                string worked = _service.Delete(id, userInfo.Id);
                // if (worked != null)
                // {
                //     // _ks.DecreaseKeeps(id);
                // }

                return Ok(worked);
            }
            catch (Forbidden e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
        }

        // public EventHandler VaultKeepDel
        // public abstract class EventListener VaultKeepDeleted; 


    }
}