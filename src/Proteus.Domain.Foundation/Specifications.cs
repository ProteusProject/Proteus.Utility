/*
 *
 * Proteus
 * Copyright (C) 2008, 2009
 * Stephen A. Bohlen
 * http://www.unhandled-exceptions.com
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
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