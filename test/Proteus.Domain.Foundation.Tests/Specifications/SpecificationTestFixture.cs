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


using System.Linq;
using NUnit.Framework;
using Proteus.Domain.Foundation.Tests.Specifications.Domain.Base;
using Proteus.Domain.Foundation.Tests.Specifications.Infrastructure;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications
{
    public abstract class SpecificationTestFixture<T> : SpecificationTestFixture<T, T> where T : class, IEntity
    {
    }

    public abstract class SpecificationTestFixture<T, TResult> where T : class, IEntity
    {
        protected static void SpecificationDoesMatch(T instance, TResult expected,
                                                     ISpecification<T, TResult> specification)
        {
            IQueryable<TResult> matching = specification.SatisfyingElementsFrom(new[] { instance }.AsQueryable());
            Assert.AreEqual(1, matching.Count());
            Assert.AreEqual(expected, matching.First());
        }

        protected static void SpecificationDoesMatch(T instance, ISpecification<T, TResult> specification)
        {
            IQueryable<TResult> matching = specification.SatisfyingElementsFrom(new[] { instance }.AsQueryable());
            Assert.AreEqual(1, matching.Count());
            Assert.AreEqual(instance, matching.Cast<T>().First());
        }

        protected static void SpecificationDoesMatchViaRepository(TResult expected,
                                                                  ISpecification<T, TResult> specification)
        {
            IQueryable<TResult> matching = Repository<T>.FindAll(specification);
            Assert.AreEqual(1, matching.Count());
            Assert.AreEqual(expected, matching.First());
        }

        protected static void SpecificationDoesNotMatch(T instance, ISpecification<T, TResult> specification)
        {
            IQueryable<TResult> matching = specification.SatisfyingElementsFrom(new[] { instance }.AsQueryable());
            Assert.AreEqual(0, matching.Count());
        }

        protected static void SpecificationDoesNotMatchViaRepository(ISpecification<T, TResult> specification)
        {
            Assert.IsNull(Repository<T>.FindOne(specification));
        }
    }
}