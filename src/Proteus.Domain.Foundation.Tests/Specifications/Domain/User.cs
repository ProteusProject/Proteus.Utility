/*
 *
 * Proteus
 * Copyright (c) 2010 - 2011
 * Stephen A. Bohlen
 * http://www.unhandled-exceptions.com
 *
 * Portions Copyright (C) 2008
 * Steve Burman
 * Linq.Specifications http://code.google.com/p/linq-specifications/
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 */


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