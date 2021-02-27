using System;
using System.Linq;
using System.Collections.Generic;
using finalcheckpoint_server.Models;
using finalcheckpoint_server.Repositories;
using finalcheckpoint_server.Exceptions;

namespace finalcheckpoint_server.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _krepo;
        public KeepsService(KeepsRepository krepo)
        {
            _krepo = krepo;
        }

        internal IEnumerable<Keep> GetKeeps()
        {
            return _krepo.GetKeeps();
        }
        internal Keep GetById(int id)
        {
            Keep exists = _krepo.Get(id);
            if (exists == null)
            {
                throw new Exception("Invalid Id");
            }
            return exists;
        }

        internal IEnumerable<Keep> GetByProfileId(string id)
        {
            return _krepo.GetByOwnerId(id);
        }

        internal Keep Create(Keep newKeep)
        {
            int id = _krepo.Create(newKeep);
            newKeep.Id = id;
            return newKeep;
        }

        internal Keep Edit(Keep editData, string userId)
        {
            Keep original = GetById(editData.Id);
            if (original.CreatorId != userId) { throw new Forbidden("Access Denied: You are not the original creator"); }
            editData.Name = editData.Name == null ? original.Name : editData.Name;
            editData.Description = editData.Description == null ? original.Description : editData.Description;
            editData.Img = editData.Img == null ? original.Img : editData.Img;

            return _krepo.Edit(editData);

        }
        internal string Delete(int id, string userId)
        {
            Keep original = _krepo.Get(id);
            if (original == null) { throw new Exception("Bad Id"); }
            if (original.CreatorId != userId) { throw new Forbidden("Access Denied: You are not the original creator"); }
            _krepo.Remove(id);
            return "successfully deleted";
        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            IEnumerable<VaultKeepViewModel> data = _krepo.GetKeepsByVaultId(id);
            return data;

        }


    }
}