using System;
using System.Linq;

namespace Proteus.Domain.Foundation.Specifications
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Takes an IQueryable instance of type T and returns an IQueryable of type TResult.
        /// The Func provided as the resultMap parameter is applied to each element in the original collection.
        /// </summary>
        public static IQueryable<TResult> Convert<T, TResult>(this IQueryable<T> queryable,
                                                              Converter<T, TResult> resultMap)
        {
            return from q in queryable select resultMap(q);
        }
    }
}