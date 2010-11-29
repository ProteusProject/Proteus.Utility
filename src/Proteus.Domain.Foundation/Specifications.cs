/*
 *
 * Proteus
 * Copyright (C) 2008, 2009, 2010
 * Stephen A. Bohlen
 * http://www.unhandled-exceptions.com
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
using System.Text;

namespace Proteus.Domain.Foundation
{
    /// <summary>
    /// Default implementation of ICompositeSpecification&lt;T&gt;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CompositeSpecification<T> : ICompositeSpecification<T>
    {

        /// <summary>
        /// Ands the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public ICompositeSpecification<T> And(ICompositeSpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        /// <summary>
        /// Determines whether [is satisfied by] [the specified candidate].
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>
        /// 	<c>true</c> if [is satisfied by] [the specified candidate]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsSatisfiedBy(T candidate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Nots this instance.
        /// </summary>
        /// <returns></returns>
        public ICompositeSpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        /// <summary>
        /// Ors the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public ICompositeSpecification<T> Or(ICompositeSpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

    }

    /// <summary>
    /// Specification defined by the logical conjugate AND
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AndSpecification<T> : CompositeSpecification<T>
    {

        private ICompositeSpecification<T> _one;

        private ICompositeSpecification<T> _other;

        /// <summary>
        /// Initializes a new instance of the <see cref="AndSpecification&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="one">The first specification</param>
        /// <param name="other">The second specification</param>
        public AndSpecification(ICompositeSpecification<T> one, ICompositeSpecification<T> other)
        {
            _one = one;
            _other = other;
        }

        /// <summary>
        /// Determines whether [is satisfied by] [the specified candidate].
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>
        /// 	<c>true</c> if [is satisfied by] [the specified candidate]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsSatisfiedBy(T candidate)
        {
            return _one.IsSatisfiedBy(candidate) && _other.IsSatisfiedBy(candidate);
        }

    }

    /// <summary>
    /// Specification defined by the logical conjugate OR
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OrSpecification<T> : CompositeSpecification<T>
    {

        private ICompositeSpecification<T> _one;

        private ICompositeSpecification<T> _other;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrSpecification&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="one">The first specification</param>
        /// <param name="other">The second specification</param>
        public OrSpecification(ICompositeSpecification<T> one, ICompositeSpecification<T> other)
        {
            _one = one;
            _other = other;
        }

        /// <summary>
        /// Determines whether [is satisfied by] [the specified candidate].
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>
        /// 	<c>true</c> if [is satisfied by] [the specified candidate]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsSatisfiedBy(T candidate)
        {
            return _one.IsSatisfiedBy(candidate) || _other.IsSatisfiedBy(candidate);
        }

    }

    /// <summary>
    /// Specification defined by the logical conjugate NOT
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotSpecification<T> : CompositeSpecification<T>
    {

        private ICompositeSpecification<T> wrapped;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotSpecification&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="specification">The specification.</param>
        public NotSpecification(ICompositeSpecification<T> specification)
        {
            wrapped = specification;
        }

        /// <summary>
        /// Determines whether [is satisfied by] [the specified candidate].
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>
        /// 	<c>true</c> if [is satisfied by] [the specified candidate]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsSatisfiedBy(T candidate)
        {
            return !wrapped.IsSatisfiedBy(candidate);
        }

    }

}