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
        internal Keep GetById(int id, string userId)
        {
            return _krepo.GetById(id);
        }
        internal Keep GetByIdNoProf(int id)
        {
            Keep exists = _krepo.GetById(id);
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
            Keep original = GetByIdNoProf(editData.Id);
            if (original.CreatorId != userId)
            {
                throw new Forbidden("Access Denied: You are not the original creator");
            }
            editData.Name = editData.Name == null ? original.Name : editData.Name;
            editData.Description = editData.Description == null ? original.Description : editData.Description;
            editData.Img = editData.Img == null ? original.Img : editData.Img;

            return _krepo.Edit(editData);

        }
        // internal Keep IncreaseViews(Keep keep)
        // {
        //     int originalKeeps = keep.Keeps;
        //     // if (keep.CreatorId == userId)
        //     // {
        //     //     throw new Forbidden("Thought you'd catch me slippin");
        //     // }
        //     // if (keep.CreatorId != userId)
        //     // {
        //     // Keep found = GetByIdNoProf(keep.Id);
        //     for (int i = originalKeeps; i <= originalKeeps; i++)
        //     {
        //         if (i > originalKeeps)
        //         {
        //             break;
        //         }
        //         keep.Keeps += 1;
        //         if (i == originalKeeps)
        //         {
        //             continue;
        //         }
        //     }
        //     // }
        //     return _krepo.EditViews(keep);
        // }
        internal Keep IncreaseViews(Keep keep)
        {
            int originalViews = keep.Views;
            // if (keep.CreatorId == userId)
            // {
            //     throw new Forbidden("Thought you'd catch me slippin");
            // }
            // if (keep.CreatorId != userId)
            // {
            for (int i = originalViews; i <= originalViews; i++)
            {
                if (i > originalViews)
                {
                    break;
                }
                keep.Views += 1;
                if (i == originalViews)
                {
                    continue;
                }
            }
            // }
            return _krepo.EditViews(keep);
        }

        internal Keep IncreaseKeeps(int keepId, string vkCreatorId)
        {
            Keep original = _krepo.GetById(keepId);
            Keep found = _krepo.GetById(keepId);
            if (found.CreatorId != vkCreatorId)
            {

                // do
                // {

                for (int i = found.Keeps; i <= original.Keeps; i++)
                {
                    if (i > found.Keeps)
                    {
                        break;
                    }
                    found.Keeps += 1;
                    if (i == found.Keeps)
                    {
                        continue;
                    }
                }
                // if (keep.CreatorId == userId)
                // {
                //     throw new Forbidden("Thought you'd catch me slippin");
                // }
                // if (keep.CreatorId != userId)
                // {
                // //    this is where it was before
                // }
                // } while (original.Keeps > found.Keeps);
            }
            return _krepo.EditKeeps(found);
        }
        internal string Delete(int id, string userId)
        {
            Keep original = _krepo.GetById(id);
            if (original == null) { throw new Exception("Bad Id"); }
            if (original.CreatorId != userId) { throw new Forbidden("Access Denied: You are not the original creator"); }
            _krepo.Remove(id);
            return "successfully deleted";
        }

        // internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id)
        // {
        //     IEnumerable<VaultKeepViewModel> data = _krepo.GetKeepsByVaultId(id);
        //     return data;

        // }


    }
}