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
using System.Linq;
using System.Linq.Expressions;

namespace Proteus.Domain.Foundation.Specifications
{
    public abstract class Specification<T> : Specification<T, T>
    {
        public override Converter<T, T> ResultMap
        {
            get { return t => t; }
        }
    }

    public abstract class Specification<T, TResult> : QuerySpecification<T, TResult>
    {
        public virtual Expression<Func<T, object>> OrderingCriteria
        {
            get { return null; }
        }

        public virtual QueryOrder OrderingDirection
        {
            get { return QueryOrder.Ascending; }
        }

        public virtual int? NumberOfResults
        {
            get { return null; }
        }

        public override IQueryable<TResult> SatisfyingElementsFrom(IQueryable<T> candidates)
        {
            var result = candidates;

            if (Predicate != null)
                result = candidates.Where(Predicate);

            if (OrderingCriteria != null)
            {
                result = (OrderingDirection == QueryOrder.Ascending)
                             ? result.OrderBy(OrderingCriteria)
                             : result.OrderByDescending(OrderingCriteria);
            }

            if (NumberOfResults.HasValue)
                result = result.Take(NumberOfResults.Value);

            return result.Convert(ResultMap);
        }
    }
}