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