using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using finalcheckpoint_server.Models;
using finalcheckpoint_server.Services;

namespace finalcheckpoint_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _service;
        private readonly KeepsService _ks;

        private readonly VaultsService _vs;

        public ProfilesController(ProfilesService service, KeepsService ks, VaultsService vs)
        {
            _service = service;
            _ks = ks;
            _vs = vs;
        }


        // This is a get by ID
        [HttpGet("{id}")]
        public ActionResult<Profile> Get(string id)
        {
            try
            {
                Profile profile = _service.GetProfileById(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]

        public ActionResult<IEnumerable<Keep>> GetKeepsByProfileId(string id)
        {
            try
            {
                IEnumerable<Keep> keeps = _ks.GetByProfileId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]

        public ActionResult<IEnumerable<Vault>> GetVaultsByProfileId(string id)
        {
            try
            {
                IEnumerable<Vault> keeps = _vs.GetVaultsByProfileId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        // [HttpGet(("{id}/parties"))]
        // [Authorize]
        // public ActionResult<IEnumerable<PartyPartyMemberViewModel>> GetParties(string id)
        // {
        //     try
        //     {
        //         IEnumerable<PartyPartyMemberViewModel> parties = _ps.GetByProfileId(id);
        //         return Ok(parties);
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

    }
}