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


using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.Domain.Foundation.Tests.Specifications.Domain.Base;
using Proteus.Domain.Foundation.Tests.Specifications.Domain.Services;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications.Infrastructure
{
    public class MemoryRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly Dictionary<Guid, T> items = new Dictionary<Guid, T>();

        #region IRepository<T> Members

        public void Save(T instance)
        {
            if (instance.Id == Guid.Empty)
                instance.Id = Guid.NewGuid();

            if (items.ContainsKey(instance.Id))
            {
                items[instance.Id] = instance;
            }
            else
            {
                items.Add(instance.Id, instance);
            }
        }

        public void Delete(T instance)
        {
            if (items.ContainsKey(instance.Id))
                items.Remove(instance.Id);
        }

        public TResult FindOne<TResult>(ISpecification<T, TResult> specification)
        {
            return FindAll(specification).SingleOrDefault();
        }

        public IQueryable<TResult> FindAll<TResult>(ISpecification<T, TResult> specification)
        {
            return specification.SatisfyingElementsFrom(items.Values.AsQueryable());
        }

        public IQueryable<T> FindAll()
        {
            return items.Values.AsQueryable();
        }

        public void Clear()
        {
            items.Clear();
        }

        #endregion
    }
}