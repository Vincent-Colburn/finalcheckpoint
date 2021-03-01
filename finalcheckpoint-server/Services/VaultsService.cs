using System;
using System.Linq;
using System.Collections.Generic;
using finalcheckpoint_server.Models;
using finalcheckpoint_server.Repositories;
using finalcheckpoint_server.Exceptions;

namespace finalcheckpoint_server.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vrepo;

        private readonly ProfilesRepository _prepo;
        public VaultsService(VaultsRepository vrepo, ProfilesRepository prepo)
        {
            _vrepo = vrepo;
            _prepo = prepo;
        }



        internal IEnumerable<Vault> GetAll()
        {

            IEnumerable<Vault> vaults = _vrepo.GetAll();
            return vaults;
        }


        internal Vault GetById(int id)
        {

            var exists = _vrepo.GetById(id);
            if (exists.IsPrivate == false)
            {
                return exists;
            }
            if (exists.IsPrivate == true)
            {
                throw new Forbidden("This vault is private");
            }
            return exists;
        }

        internal Vault Create(Vault newVault)
        {
            return _vrepo.Create(newVault);
        }

        internal Vault Edit(Vault editData, string userId)
        {
            Vault original = GetById(editData.Id);
            if (original.CreatorId != userId) { throw new Forbidden("Access Denied: You are not the original creator"); }
            editData.Name = editData.Name == null ? original.Name : editData.Name;
            editData.Description = editData.Description == null ? original.Description : editData.Description;

            return _vrepo.Edit(editData);
        }

        internal string Delete(int id, string userId)
        {
            Vault original = _vrepo.GetById(id);
            if (original == null)
            {
                throw new Exception("Bad Id");
            }
            if (original.CreatorId != userId)
            {
                throw new Forbidden("Access Denied: You are not the original creator");
            }
            _vrepo.Remove(id);
            return "sucessfully deleted";
        }

        // internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id)
        // {
        //     // Vault original = _vrepo.GetById(id);
        //     IEnumerable<VaultKeepViewModel> data = _vrepo.GetKeepsByVaultId(id).ToList();
        //     // if (original.IsPrivate == false)
        //     // {
        //     //     return data;
        //     // }
        //     // if (original.IsPrivate == true)
        //     // {
        //     //     throw new Forbidden("This vault is private");
        //     // }
        //     return data;



        // }

        internal IEnumerable<Vault> GetVaultsByProfileId(string id)
        {
            return _vrepo.GetVaultsByProfileId(id).ToList().FindAll(v => v.IsPrivate == false);
        }
        internal IEnumerable<Vault> GetVaultsByOwnerId(string id)
        {
            return _vrepo.GetVaultsByOwnerId(id);

        }



    }
}