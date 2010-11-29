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