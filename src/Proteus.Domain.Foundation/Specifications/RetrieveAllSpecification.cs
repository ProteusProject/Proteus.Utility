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

namespace Proteus.Domain.Foundation.Specifications
{
    public class RetrieveAllSpecification<T> : ISpecification<T>
    {
        #region ISpecification<T> Members

        public IQueryable<T> SatisfyingElementsFrom(IQueryable<T> candidates)
        {
            return from c in candidates select c;
        }

        public bool IsSatisfiedBy(T candidate)
        {
            return true;
        }
        #endregion


        
    }
}