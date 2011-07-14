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
    public static class QuerySpecificationExtensions
    {
        public static QuerySpecification<T> And<T>(this QuerySpecification<T> specification1,
                                                   QuerySpecification<T> specification2)
        {
            var adHocSpec1 = new AdHoc<T>(specification1.Predicate);
            var adHocSpec2 = new AdHoc<T>(specification2.Predicate);

            InvocationExpression invokedExpr = Expression.Invoke(adHocSpec2.Predicate,
                                                                 adHocSpec1.Predicate.Parameters.Cast<Expression>
                                                                     ());
            Expression<Func<T, bool>> dynamicClause = Expression.Lambda<Func<T, bool>>
                (Expression.AndAlso(adHocSpec1.Predicate.Body, invokedExpr),
                 adHocSpec1.Predicate.Parameters);

            return new AdHoc<T>(dynamicClause);
        }

        public static QuerySpecification<T> Or<T>(this QuerySpecification<T> specification1,
                                                  QuerySpecification<T> specification2)
        {
            var adHocSpec1 = new AdHoc<T>(specification1.Predicate);
            var adHocSpec2 = new AdHoc<T>(specification2.Predicate);

            InvocationExpression invokedExpr = Expression.Invoke(adHocSpec2.Predicate,
                                                                 adHocSpec1.Predicate.Parameters.Cast<Expression>
                                                                     ());
            Expression<Func<T, bool>> dynamicClause = Expression.Lambda<Func<T, bool>>
                (Expression.OrElse(adHocSpec1.Predicate.Body, invokedExpr),
                 adHocSpec1.Predicate.Parameters);

            return new AdHoc<T>(dynamicClause);
        }
    }
}