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