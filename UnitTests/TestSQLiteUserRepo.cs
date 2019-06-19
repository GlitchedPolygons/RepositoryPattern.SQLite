using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GlitchedPolygons.RepositoryPattern.SQLite;

namespace UnitTests
{
    public class TestSQLiteUserRepo : SQLiteRepository<User, string>
    {
        public TestSQLiteUserRepo(string connectionString, string tableName = null) : base(connectionString, tableName)
        {
        }

        public override async Task<bool> Add(User entity)
        {
            using (var dbcon = OpenConnection())
            {
                string sql = $"INSERT INTO \"{TableName}\" VALUES (@Id, @FullName, @Address, @PhoneNumber)";
                int result = await dbcon.ExecuteAsync(sql, new {Id = entity.Id, entity.FullName, entity.Address, entity.PhoneNumber});
                return result > 0;
            }
        }

        public override Task<bool> AddRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Update(User entity)
        {
            var sql = new StringBuilder(256)
                .Append($"UPDATE \"{TableName}\" SET ")
                .Append("\"FullName\" = @FullName, ")
                .Append("\"Address\" = @Address, ")
                .Append("\"PhoneNumber\" = @PhoneNumber ")
                .Append("WHERE \"Id\" = @Id");
            
            using (var dbcon = OpenConnection())
            {
                int result = await dbcon.ExecuteAsync(sql.ToString(), new
                {
                    Id = entity.Id, 
                    entity.FullName, 
                    entity.Address, 
                    entity.PhoneNumber
                });
                
                return result > 0;
            }
        }
    }
}
