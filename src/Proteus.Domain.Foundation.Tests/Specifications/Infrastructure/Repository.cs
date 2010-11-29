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


using System.Linq;
using Proteus.Domain.Foundation.Tests.Specifications.Domain.Base;
using Proteus.Domain.Foundation.Tests.Specifications.Domain.Services;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications.Infrastructure
{
    public static class Repository<T> where T : IEntity
    {
        public static IRepository<T> inner;

        static Repository()
        {
            inner = new MemoryRepository<T>();
        }

        public static void Save(T instance)
        {
            inner.Save(instance);
        }

        public static void Delete(T instance)
        {
            inner.Delete(instance);
        }

        public static TResult FindOne<TResult>(ISpecification<T, TResult> specification)
        {
            return inner.FindOne(specification);
        }

        public static IQueryable<TResult> FindAll<TResult>(ISpecification<T, TResult> specification)
        {
            return inner.FindAll(specification);
        }

        public static IQueryable<T> FindAll()
        {
            return inner.FindAll();
        }

        public static void Clear()
        {
            inner.Clear();
        }
    }
}