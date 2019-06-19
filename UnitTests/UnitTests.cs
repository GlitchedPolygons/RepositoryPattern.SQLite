using System;
using System.IO;
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
        public async Task T()
        {
            
        }
    }
}