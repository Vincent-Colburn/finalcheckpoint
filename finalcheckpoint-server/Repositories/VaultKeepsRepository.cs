using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using finalcheckpoint_server.Models;

namespace finalcheckpoint_server.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        // internal IEnumerable<VaultKeep> Get()
        // {
        //     string sql = "SELECT * FROM vaultkeeps;";
        //     return _db.Query<VaultKeep>(sql);
        // }
        internal int Create(VaultKeep newVau)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@creatorId, @VaultId, @KeepId);
            SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newVau);
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id;";
            _db.Execute(sql, new { id });
        }
    }
}