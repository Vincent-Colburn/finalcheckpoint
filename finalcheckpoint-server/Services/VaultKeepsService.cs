using System;
using System.Collections.Generic;
using finalcheckpoint_server.Models;
using finalcheckpoint_server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace finalcheckpoint_server.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;

        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }

        // internal IEnumerable<VaultKeep> Get()
        // {
        //     return _repo.Get();
        // }

        internal VaultKeep Create(VaultKeep newVau)
        {
            int id = _repo.Create(newVau);
            newVau.Id = id;
            return newVau;
        }

        public string Delete(int id)
        {
            _repo.Delete(id);
            return "Delete Successful";
        }
    }
}