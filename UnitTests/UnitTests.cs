using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using GlitchedPolygons.RepositoryPattern.SQLite;

namespace UnitTests
{
    public class UnitTests
    {
        private readonly string path;
        private readonly SQLiteRepository<User, string> repo;
        
        public UnitTests()
        {
            path = Path.Combine(Environment.CurrentDirectory, "TestDatabase.db");
            repo = new TestSQLiteUserRepo($"Data Source={path};Version=3;");
        }
        
        [Fact]
        public void EnsureTestDbExists()
        {
            Assert.True(File.Exists(path));
        }

        [Fact]
        public void GetById_UserExists_InstanceNotNull_DataIsValid()
        {
            User test = repo["TEST"];
            
            Assert.NotNull(test);
            Assert.Equal("TEST", test.Id);
            Assert.Equal("TEST", test.FullName);
            Assert.Equal("TEST", test.Address);
            Assert.Equal(0, test.PhoneNumber);
        }

        [Fact]
        public async Task GetByIdAsync_UserExists_InstanceNotNull_DataIsValid()
        {
            User test = await repo.Get("TEST");
            
            Assert.NotNull(test);
            Assert.Equal("TEST", test.Id);
            Assert.Equal("TEST", test.FullName);
            Assert.Equal("TEST", test.Address);
            Assert.Equal(0, test.PhoneNumber);
        }

        [Fact]
        public async Task GetAll_EntriesExist_MoreThanFive_ContainsTEST()
        {
            var users = await repo.GetAll();
            var enumerable = users as User[] ?? users.ToArray();
            Assert.True(enumerable.Count() > 5);
            Assert.Contains(enumerable, u => u.Id.Equals("TEST") && u.FullName.Equals("TEST") && u.Address.Equals("TEST") && u.PhoneNumber == 0);
        }
    }
}