using System;
using System.Collections.Generic;
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
        public void GetById_UserDoesNotExist_InstanceNull()
        {
            User test = repo["WRONG"];
            Assert.Null(test);
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

        [Fact]
        public async Task Add_RowIsAdded_DbContainsEntry()
        {
            var user = new User {Address = "address", FullName = "name", PhoneNumber = 47};
            bool success = await repo.Add(user);
            Assert.True(success);
            Assert.NotNull(repo[user.Id]);
            var _user = await repo.Get(user.Id);
            Assert.NotNull(_user);
            Assert.Equal(_user.Id, user.Id);
            Assert.Equal(_user.Address, user.Address);
            Assert.Equal(_user.FullName, user.FullName);
            Assert.Equal(_user.PhoneNumber, user.PhoneNumber);
            await repo.Remove(user.Id);
        }

        [Fact]
        public async Task AddRange_RowsAreAdded_DbContainsEntries()
        {
            var users = new List<User>()
            {
                new User {Address = "address0", FullName = "name0", PhoneNumber = 47},
                new User {Address = "address1", FullName = "name1", PhoneNumber = 48},
                new User {Address = "address2", FullName = "name2", PhoneNumber = 49},
                new User {Address = "address3", FullName = "name3", PhoneNumber = 50},
            };
            
            bool success = await repo.AddRange(users);
            Assert.True(success);

            for (var i = 0; i < users.Count; i++)
            {
                var user = users[i];
                Assert.NotNull(user.Id);
                Assert.Equal(user.PhoneNumber, 47 + i);
                Assert.EndsWith(i.ToString(), user.Address);
                Assert.EndsWith(i.ToString(), user.FullName);
            }

            await repo.RemoveRange(users);
        }

        [Fact]
        public async Task Remove_TestRowIsDeleted_DbDoesNotContainEntry()
        {
            var temp = new User();
            await repo.Add(temp);
            bool success = await repo.Remove(temp);
            Assert.True(success);
            Assert.DoesNotContain(await repo.GetAll(), u => u.Id == temp.Id);
        }
        
        [Fact]
        public async Task RemoveById_TestRowIsDeleted_DbDoesNotContainEntry()
        {
            var temp = new User();
            await repo.Add(temp);
            bool success = await repo.Remove(temp.Id);
            Assert.True(success);
            Assert.DoesNotContain(await repo.GetAll(), u => u.Id == temp.Id);
        }
    }
}