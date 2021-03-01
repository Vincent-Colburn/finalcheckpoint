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

        internal VaultKeep GetById(int id)
        {
            VaultKeep exists = _repo.GetById(id);
            if (exists == null)
            {
                throw new Exception("Invalid Id");
            }
            return exists;
        }

        internal VaultKeep Create(VaultKeep newVau)
        {
            Vault found = _vrepo.GetById(newVau.VaultId);
            if (found.CreatorId == newVau.CreatorId)
            {
                int id = _repo.Create(newVau);
                newVau.Id = id;
                return newVau;
            }
            else
            {
                throw new Forbidden("You can't add keeps to vaults you don't own");
            }

        }

        // internal string Delete(int id, string userId)
        // {
        //     var data = _repo.GetById(id);
        //     if (data == null)
        //     {
        //         throw new Exception("Invalid Id");
        //     }
        //     if (userId != data.CreatorId)
        //     {
        //         throw new Forbidden("You cannot delete what you did not create");
        //     }
        //     _repo.Delete(id);
        //     return "Delete Successful";
        // }

        internal string Delete(int id, string userId)
        {
            VaultKeep original = _repo.GetById(id);
            if (original == null) { throw new Exception("Bad Id"); }
            if (original.CreatorId != userId) { throw new Forbidden("Access Denied: You are not the original creator"); }
            _repo.Delete(id);
            return "Delete Successful";
        }
    }
}