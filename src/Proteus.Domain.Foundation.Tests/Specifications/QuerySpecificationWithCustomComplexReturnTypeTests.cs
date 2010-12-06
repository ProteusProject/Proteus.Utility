/*
 *
 * Proteus
 * Copyright (C) 2010
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


using System;
using System.Linq.Expressions;
using MbUnit.Framework;
using Proteus.Domain.Foundation.Tests.Specifications.Domain;
using Proteus.Domain.Foundation.Tests.Specifications.Infrastructure;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications
{
    public class QuerySpecificationWithCustomComplexReturnTypeTests : SpecificationTestFixture<Blog, User>
    {
        private Blog blog1;
        private Blog blog2;
        private Blog blog3;
        private User user1;
        private User user2;
        private User user3;

        [SetUp]
        public void TestSetup()
        {
            Repository<Blog>.Clear();
            Repository<User>.Clear();

            user1 = new User("user1");
            user2 = new User("user2");
            user3 = new User("user3");

            blog1 = new Blog("blog1");
            blog2 = new Blog("blog2");
            blog3 = new Blog("blog3");

            blog1.Author = user1;
            blog2.Author = user2;
            blog3.Author = null; // no author :(

            Repository<Blog>.Save(blog1);
            Repository<Blog>.Save(blog2);
            Repository<Blog>.Save(blog3);

            Repository<User>.Save(user1);
            Repository<User>.Save(user2);
            Repository<User>.Save(user3);
        }

        [Test]
        public void AuthorMatchesAccordingToBlogName()
        {
            SpecificationDoesMatch(blog1, user1, new AuthorByBlogNameSpecification("blog1"));
            SpecificationDoesMatch(blog2, user2, new AuthorByBlogNameSpecification("blog2"));
            SpecificationDoesMatch(blog3, null, new AuthorByBlogNameSpecification("blog3"));
        }

        [Test]
        public void AuthorMatchesAccordingToBlogNameViaRepository()
        {
            SpecificationDoesMatchViaRepository(user1, new AuthorByBlogNameSpecification("blog1"));
            SpecificationDoesMatchViaRepository(user2, new AuthorByBlogNameSpecification("blog2"));
            SpecificationDoesMatchViaRepository(null, new AuthorByBlogNameSpecification("blog3"));
        }

        [Test]
        public void NonExistentBlogNameDoesNotMatchAny()
        {
            var specification = new AuthorByBlogNameSpecification("no_match");
            SpecificationDoesNotMatch(blog1, specification);
            SpecificationDoesNotMatch(blog2, specification);
            SpecificationDoesNotMatch(blog3, specification);
        }

        [Test]
        public void NonExistentBlogNameDoesNotMatchAnyViaRepository()
        {
            SpecificationDoesNotMatchViaRepository(new AuthorByBlogNameSpecification("no_match"));
        }

        #region Nested type: AuthorByBlogNameSpecification

        private class AuthorByBlogNameSpecification : QuerySpecification<Blog, User>
        {
            private readonly string blogname;

            public AuthorByBlogNameSpecification(string blogname)
            {
                this.blogname = blogname;
            }

            public override Converter<Blog, User> ResultMap
            {
                get { return b => b.Author; }
            }

            public override Expression<Func<Blog, bool>> Predicate
            {
                get { return b => b.Name == blogname; }
            }
        }

        #endregion
    }
}