using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using finalcheckpoint_server.Models;

namespace finalcheckpoint_server.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Vault> GetAll()
        {
            string sql = "SELECT * FROM vaults;";
            return _db.Query<Vault>(sql);
        }

        internal Vault GetById(int id)
        {
            string sql = @"
            SELECT
            vault.*,
            profile.*
            FROM vaults vault
            JOIN profiles profile ON vault.creatorId = profile.id
            WHERE vault.Id = @id";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Vault Create(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults
            (creatorId, name, description, isPrivate)
            VALUES
            (@CreatorId, @Name, @Description, @IsPrivate);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newVault);
            newVault.Id = id;
            return newVault;
        }

        internal Vault Edit(Vault editData)
        {
            string sql = @"
            UPDATE vaults
            SET 
            name = @Name,
            description = @Description
            WHERE id = @Id;";
            _db.Execute(sql, editData);
            return editData;
        }
        internal void Remove(int id)
        {

            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            string sql = @"
          SELECT
          vau.*,
          vk.id as VaultKeepId,
          keep.*
          FROM vaultkeeps vk 
          JOIN vaults vau ON vk.vaultId = vau.id
          JOIN keeps keep ON vk.keepId = keep.id
          WHERE vk.id = @id
          ";

            return _db.Query<VaultKeepViewModel, Keep, VaultKeepViewModel>(sql, (vault, keep) =>
            {
                vault.Keeps = keep;
                vault.Keeps.Creator = keep.Creator;
                return vault;
            }, new { id }, splitOn: "id");

        }

        internal IEnumerable<Vault> GetVaultsByProfileId(string id)
        {
            string sql = @"
            SELECT
            vault.*,
            profile.*
            FROM vaults vault
            JOIN profiles profile ON vault.creatorId = profile.id
            WHERE vault.creatorId = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { id }, splitOn: "id");
        }

        internal IEnumerable<Vault> GetVaultsByOwnerId(string id)
        {
            string sql = @"
            SELECT
            vault.*,
            profile.*
            FROM vaults vault
            JOIN profiles profile ON vault.creatorId = profile.id
            WHERE vault.creatorId = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => { vault.Creator = profile; return vault; }, new { id }, splitOn: "id");
        }

        // this works


        // internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
        // {
        //     string sql = @"
        //   SELECT
        //   vau.*,
        //   vk.id as VaultKeepId,
        //   keep.*
        //   FROM vaultkeeps vk 
        //   JOIN vaults vau ON vau.id = vk.vaultId
        //   JOIN keeps keep ON vk.keepId = keep.id
        //   WHERE vaultId = @vaultId
        //   ";

        //     return _db.Query<VaultKeepViewModel, Keep, VaultKeepViewModel>(sql, (vault, keep) =>
        //     {
        //         vault.Keeps = keep;
        //         return vault;
        //     }, new { vaultId }, splitOn: "id");

        // }

    }
}