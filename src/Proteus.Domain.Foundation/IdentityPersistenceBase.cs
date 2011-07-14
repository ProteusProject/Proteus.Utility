/*
 *
 * Proteus
 * Copyright (C) 2008, 2009, 2010, 2011
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
using System.ComponentModel;

namespace Proteus.Domain.Foundation
{
    /// <summary>
    /// Abstract Base Class for Persistent Entities
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <typeparam name="TIdentity">The type of the identity.</typeparam>
    [Serializable]
    public abstract class IdentityPersistenceBase<TObject, TIdentity> : IPersistentObject where TObject: class 
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