using System.Collections.Generic;
using Proteus.Domain.Foundation.Tests.Specifications.Domain.Base;

namespace Proteus.Domain.Foundation.Tests.Specifications.Domain
{
    public class User : EntityBase
    {
        private readonly string username;
        private readonly IList<Blog> blogs;

        public User(string username)
        {
            this.username = username;
            this.blogs = new List<Blog>();
        }

        public string Username
        {
            get { return username; }
        }

        public IEnumerable<Blog> Blogs
        {
            get { return blogs; }
        }

        public void AddBlog(Blog blog)
        {
            blogs.Add(blog);
            blog.Author = this;
        }
    }
}