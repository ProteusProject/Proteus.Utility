using Proteus.Domain.Foundation.Tests.Specifications.Domain.Base;

namespace Proteus.Domain.Foundation.Tests.Specifications.Domain
{
    public class Blog : EntityBase
    {
        private User author;
        private string name;

        public Blog(string name)
        {
            this.name = name;
        }

        public User Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}