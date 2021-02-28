using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using finalcheckpoint_server.Models;

namespace finalcheckpoint_server.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Keep> GetKeeps()
        {
            string sql = @"
            SELECT 
            keep.*,
            profile.*
            FROM keeps keep
            JOIN profiles profile ON keep.creatorId = profile.id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
             {
                 keep.Creator = profile;
                 return keep;
             }, splitOn: "id");
        }

        internal Keep Get(int id)
        {
            string sql = @"
            SELECT
            keep.*,
            profile.*
            FROM keeps keep
            JOIN profiles profile ON keep.creatorId = profile.id
            WHERE keep.id = @id";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal int Create(Keep newKeep)
        {
            string sql = @"
      INSERT INTO keeps
      (creatorId, name, description, img, keeps, views)
      VALUES
      (@CreatorId, @Name, @Description, @Img, @Keeps, @Views);
      SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newKeep);
        }

        // this is for getting vaults related to a profile
        // internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultById(int id)
        // {
        //     string sql = @"
        //     SELECT k.*,
        //     vk.id as VaultKeepId
        //     FROM vaultkeeps vk
        //     JOIN keeps k ON k.id == vk.vaultId
        //     JOIN profiles pr ON k.creatorId = pr.id
        //     WHERE vaultId = @vaultId
        //    ";
        //     return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vault, profile) =>
        //     {
        //         vault.Creator = profile;
        //         return vault;
        //     }, new { id }, splitOn: "id");
        // }

        // This for getting a profiles keeps
        internal IEnumerable<Keep> GetByOwnerId(string id)
        {
            string sql = @"
            SELECT
            keep.*,
            profile.*
            FROM keeps keep
            JOIN profiles profile ON keep.creatorId = profile.id
            WHERE keep.creatorId = @id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;

            }, new { id }, splitOn: "id");
        }


        internal Keep Edit(Keep editData)
        {
            string sql = @"
            UPDATE keeps
            SET 
            name = @Name,
            description = @Description,
            img = @Img
            WHERE id = @Id;";
            _db.Execute(sql, editData);
            return editData;
        }
        internal void Remove(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

        // TODO NOT WORKING
        // internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
        // {
        //     string sql = @"
        //   SELECT
        //   vau.*,
        //   vk.id as VaultKeepId,
        //   keep.*
        //   FROM vaultkeeps vk 
        //   JOIN vaults vau ON vau.id == vk.vaultId
        //   JOIN keeps keep ON vk.keepId == keep.id
        //   ";

        //     return _db.Query<VaultKeepViewModel, Keep, VaultKeepViewModel>(sql, (vault, keep) =>
        //     {
        //         vault.Keeps = keep;
        //         return vault;
        //     }, new { vaultId }, splitOn: "id");
        // }
    }
}