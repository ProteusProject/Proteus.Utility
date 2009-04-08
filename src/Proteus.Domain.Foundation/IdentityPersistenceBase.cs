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
using System.ComponentModel;

namespace Proteus.Domain.Foundation
{
    /// <summary>
    /// Abstract Base Class for Persistent Entities
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <typeparam name="TIdentity">The type of the identity.</typeparam>
    public abstract class IdentityPersistenceBase<TObject, TIdentity> : IPersistentObject
    {
        /// <summary>
        /// Last-updated-by value for the Entity
        /// </summary>
        protected object _lastUpdatedBy = null;

        /// <summary>
        /// Last-updated-date-time for the Entity
        /// </summary>
        protected DateTime _lastUpdatedDateTime = DateTime.Now;

        /// <summary>
        /// Identity Persistence value for the Entity
        /// </summary>
        protected TIdentity _persistenceId;

        /// <summary>
        /// Persistence Version for the Entity
        /// </summary>
        protected int _persistenceVersion;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(IdentityPersistenceBase<TObject, TIdentity> left, IdentityPersistenceBase<TObject, TIdentity> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(IdentityPersistenceBase<TObject, TIdentity> left, IdentityPersistenceBase<TObject, TIdentity> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is transient.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is transient; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsTransient
        {
            get
            {
                if (_persistenceId.GetType() == typeof(Guid))
                    return (Guid)TypeDescriptor.GetConverter(_persistenceId).ConvertFrom(_persistenceId.ToString()) == Guid.Empty;

                double testDouble;

                if (Double.TryParse(_persistenceId.ToString(), out testDouble))
                    return (double)TypeDescriptor.GetConverter(typeof(double)).ConvertFrom(_persistenceId.ToString()) == 0d;

                if (_persistenceId.GetType() == (typeof(string)))
                    return (string)TypeDescriptor.GetConverter(_persistenceId).ConvertFrom(_persistenceId.ToString()) == String.Empty;

                //if we get this far, we have a non-GUID, non-numeric, non string identity type and this is unsupported, so throw...
                throw new ArgumentException("IdentityPersistenceBase<TObject, TIdentity> Class only provides native support for Guid, Numeric (Int, Int32, Int64, Double, etc.) or String as the TIdentity type.  For other types (including any custom types), you *must* override the IsTransient virtual property to provide your own implementation!", "TIdentity");
            }
        }

        /// <summary>
        /// Gets the last updated by.
        /// </summary>
        /// <value>The last updated by.</value>
        public virtual object LastUpdatedBy
        {
            get { return _lastUpdatedBy; }

        }

        /// <summary>
        /// Gets the last updated date time.
        /// </summary>
        /// <value>The last updated date time.</value>
        public virtual DateTime LastUpdatedDateTime
        {
            get { return _lastUpdatedDateTime; }

        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            if ((other != null) && (other is TObject) && (ReferenceEquals(this, other)))
                return true;

            if ((other == null) || (!(other is TObject)) || (IsTransient))
                return false;

            return (GetHashCode() == other.GetHashCode());

        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return string.Format("{0}|{1}", GetType().ToString(), _persistenceId).GetHashCode();
        }

    }
}