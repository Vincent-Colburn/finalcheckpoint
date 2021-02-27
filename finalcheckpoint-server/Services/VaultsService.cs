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
        public VaultsService(VaultsRepository vrepo)
        {
            _vrepo = vrepo;
        }



        // internal IEnumerable<Vault> GetAll()
        // {

        //     IEnumerable<Vault> vaults = _vrepo.GetAll();
        //     return vaults.ToList().FindAll(v => v.IsPrivate);
        //     // if (exists == null)
        //     // {
        //     //     throw new Exception("Invalid Id");
        //     // }
        //     // return exists;

        // }

        // this doesn't work because it breaks all other functions that need to return a Vault
        //   internal IEnumerable<Vault> GetById(int id)
        //         {

        //             IEnumerable<Vault> exists = _vrepo.GetById(id);
        //             if (exists == null)
        //             {
        //                 throw new Exception("Invalid Id");
        //             }
        //             return exists.ToList().FindAll(v => v.IsPrivate);
        //         }

        // // // this works and is your back up
        internal Vault GetById(int id)
        {

            var exists = _vrepo.GetById(id);
            if (exists == null)
            {
                throw new Exception("Invalid Id");
            }
            return exists;
        }

        internal Vault Create(Vault newVault)
        {
            int id = _vrepo.Create(newVault);
            newVault.Id = id;
            return newVault;
        }

        internal Vault Edit(Vault editData, string userId)
        {
            Vault original = GetById(editData.Id);
            if (original.CreatorId != userId) { throw new Forbidden("Access Denied: You are not the original creator"); }
            editData.Name = editData.Name == null ? original.Name : editData.Name;
            editData.Description = editData.Description == null ? original.Description : editData.Description;

            return _vrepo.Edit(editData);

        }

        // internal object GetKeepsByVaultId(int id)
        // {
        //     throw new NotImplementedException();
        // }

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

    }
}