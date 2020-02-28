using Dapper;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GlitchedPolygons.RepositoryPattern.SQLite.Tests
{
    public class TestSQLiteUserRepo : SQLiteRepository<User, string>
    {
        public TestSQLiteUserRepo(string connectionString, string tableName = null) : base(connectionString, tableName)
        {
        }

        public override async Task<bool> Add(User entity)
        {
            if (entity is null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(entity.Id))
            {
                throw new ArgumentException($"{nameof(TestSQLiteUserRepo)}::{nameof(Add)}: The {nameof(entity)} argument's Id member has been assigned. Very bad! Entity.Id should be left alone (null or default value) because it is set by the add method.");
            }

            entity.Id = Guid.NewGuid().ToString("N");

            using var dbcon = OpenConnection();
            string sql = $"INSERT INTO \"{TableName}\" VALUES (@Id, @FullName, @Address, @PhoneNumber)";
            int result = await dbcon.ExecuteAsync(sql, new {Id = entity.Id, entity.FullName, entity.Address, entity.PhoneNumber}).ConfigureAwait(false);
            return result > 0;
        }

        public override async Task<bool> AddRange(IEnumerable<User> entities)
        {
            var sql = new StringBuilder($"INSERT INTO \"{TableName}\" VALUES ", 256);

            foreach (var entity in entities)
            {
                if (!string.IsNullOrEmpty(entity.Id))
                {
                    throw new ArgumentException($"{nameof(TestSQLiteUserRepo)}::{nameof(AddRange)}: One or more {nameof(entities)} Id member has been assigned. Very bad! Entity.Id should be left alone (null or default value) because it is set by the add method.");
                }

                entity.Id = Guid.NewGuid().ToString("N");

                sql.Append("('").Append(entity.Id).Append("', '").Append(entity.FullName).Append("', '").Append(entity.Address).Append("', '").Append(entity.PhoneNumber).Append("'),");
            }

            using var dbcon = OpenConnection();
            int result = await dbcon.ExecuteAsync(sql.ToString().TrimEnd(',')).ConfigureAwait(false);
            return result > 0;
        }

        public override async Task<bool> Update(User entity)
        {
            var sql = new StringBuilder(256)
                .Append($"UPDATE \"{TableName}\" SET ")
                .Append("\"FullName\" = @FullName, ")
                .Append("\"Address\" = @Address, ")
                .Append("\"PhoneNumber\" = @PhoneNumber ")
                .Append("WHERE \"Id\" = @Id");

            using var dbcon = OpenConnection();
            int result = await dbcon.ExecuteAsync(sql.ToString(), new
            {
                Id = entity.Id, 
                entity.FullName, 
                entity.Address, 
                entity.PhoneNumber
            }).ConfigureAwait(false);
                
            return result > 0;
        }
    }
}
