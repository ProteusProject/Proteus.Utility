using System.Linq;
using Proteus.Domain.Foundation.Tests.Specifications.Domain.Base;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications.Domain.Services
{
    public interface IRepository<T> where T : IEntity
    {
        void Save(T instance);
        void Delete(T instance);

        TResult FindOne<TResult>(ISpecification<T, TResult> specification);
        IQueryable<TResult> FindAll<TResult>(ISpecification<T, TResult> specification);
        IQueryable<T> FindAll();
        void Clear();
    }
}