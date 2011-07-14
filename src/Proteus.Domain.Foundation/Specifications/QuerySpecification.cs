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

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Proteus.Domain.Foundation.Specifications
{
    public abstract class QuerySpecification<T> : QuerySpecification<T, T>
    {
        public override Converter<T, T> ResultMap
        {
            get { return t => t; }
        }
    }

    public abstract class QuerySpecification<T, TResult> : ISpecification<T, TResult>
    {
        public abstract Converter<T, TResult> ResultMap { get; }

        public abstract Expression<Func<T, bool>> Predicate
        {
            get;
        }

        #region ISpecification<T,TResult> Members

        public virtual IQueryable<TResult> SatisfyingElementsFrom(IQueryable<T> candidates)
        {
            if (Predicate != null)
                return candidates.Where(Predicate).ToList().ConvertAll(ResultMap).AsQueryable();

            return candidates.ToList().ConvertAll(ResultMap).AsQueryable();
        }

        public virtual bool IsSatisfiedBy(T candidate)
        {
            if (Predicate == null)
                throw new InvalidOperationException("You cannot evaluate single candidate against a specification with a NULL MatchiingCriteria!");

            Func<T, bool> criteria = Predicate.Compile();

            return criteria(candidate);
        }

        #endregion



    }
}