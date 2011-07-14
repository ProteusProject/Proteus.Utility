/*
 *
 * Proteus
 * Copyright (C) 2010, 2011
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


using NUnit.Framework;
using Proteus.Domain.Foundation.Tests.Specifications.Domain;
using Proteus.Domain.Foundation.Tests.Specifications.Infrastructure;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications
{
    public class AdHocSpecificationTests : SpecificationTestFixture<User>
    {
        private User user1;
        private User user2;
        private User user3;

        [SetUp]
        public void TestSetup()
        {
            Repository<User>.Clear();

            user1 = new User("user1");
            user2 = new User("user2");
            user3 = new User("user3");

            Repository<User>.Save(user1);
            Repository<User>.Save(user2);
            Repository<User>.Save(user3);
        }

        [Test]
        public void MatchesIfNameMatchesThroughRepository()
        {
            SpecificationDoesMatchViaRepository(user1, new AdHoc<User>(u => u.Username == "user1"));
            SpecificationDoesMatchViaRepository(user2, new AdHoc<User>(u => u.Username == "user2"));
            SpecificationDoesMatchViaRepository(user3, new AdHoc<User>(u => u.Username == "user3"));
        }

        [Test]
        public void MatchesIfNameMatches()
        {
            SpecificationDoesMatch(user1, new AdHoc<User>(u => u.Username == "user1"));
            SpecificationDoesMatch(user2, new AdHoc<User>(u => u.Username == "user2"));
            SpecificationDoesMatch(user3, new AdHoc<User>(u => u.Username == "user3"));
        }

        [Test]
        public void PartialMatchDoesNotMatchAny()
        {
            var specification = new AdHoc<User>(u => u.Username == "user");
            SpecificationDoesNotMatch(user1, specification);
            SpecificationDoesNotMatch(user2, specification);
            SpecificationDoesNotMatch(user3, specification);
        }

        [Test]
        public void PartialMatchDoesNotMatchAnyViaRepository()
        {
            SpecificationDoesNotMatchViaRepository(new AdHoc<User>(u => u.Username == "user"));
        }
    }
}