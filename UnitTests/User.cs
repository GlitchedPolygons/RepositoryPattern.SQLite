using GlitchedPolygons.RepositoryPattern;

namespace UnitTests
{
    public class User : IEntity<string>
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
    }
}