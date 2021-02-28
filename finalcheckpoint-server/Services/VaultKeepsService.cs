using System;
using System.Collections.Generic;
using finalcheckpoint_server.Exceptions;
using finalcheckpoint_server.Models;
using finalcheckpoint_server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace finalcheckpoint_server.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;

        private readonly VaultsRepository _vrepo;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vrepo)
        {
            _repo = repo;
            _vrepo = vrepo;
        }

        internal IEnumerable<VaultKeep> GetAll()
        {

            IEnumerable<VaultKeep> vaultkeeps = _repo.GetAll();
            return vaultkeeps;
        }



        internal VaultKeep Create(VaultKeep newVau)
        {
            int id = _repo.Create(newVau);
            newVau.Id = id;
            return newVau;
        }

        internal string Delete(int id, string userId)
        {
            Vault original = _vrepo.GetById(id);
            if (original == null) { throw new Exception("Bad Id"); }
            if (original.CreatorId != userId) { throw new Forbidden("Access Denied: You are not the original creator"); }
            _repo.Delete(id);
            return "Delete Successful";
        }
    }
}