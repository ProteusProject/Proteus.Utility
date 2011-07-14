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
using System.Reflection;

namespace Proteus.Domain.Foundation
{
    /// <summary>
    /// Abstract Base Class for Value Objects
    /// Based on CodeCamp Server codebase http://code.google.com/p/codecampserver
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    [Serializable]
    public abstract class ValueObjectBase<TObject> : IEquatable<TObject> where TObject : class
    {

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ValueObjectBase<TObject> left, ValueObjectBase<TObject> right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);

            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ValueObjectBase<TObject> left, ValueObjectBase<TObject> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Equalses the specified candidate.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="candidate"/> parameter; otherwise, false.
        /// </returns>
        public override bool Equals(object candidate)
        {
            if (candidate == null)
                return false;

            var other = candidate as TObject;

            return Equals(other);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public virtual bool Equals(TObject other)
        {
            if (other == null)
                return false;

            Type t = GetType();

            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(other);
                object value2 = field.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if ((typeof(DateTime).IsAssignableFrom(field.FieldType)) ||
                         ((typeof(DateTime?).IsAssignableFrom(field.FieldType))))
                {
                    string dateString1 = ((DateTime)value1).ToLongDateString();
                    string dateString2 = ((DateTime)value2).ToLongDateString();
                    if (!dateString1.Equals(dateString2))
                    {
                        return false;
                    }
                    continue;
                }
                else if (!value1.Equals(value2))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            const int startValue = 17;
            const int multiplier = 59;

            int hashCode = startValue;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <returns>FieldInfo collection</returns>
        private IEnumerable<FieldInfo> GetFields()
        {
            Type t = GetType();

            var fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

                t = t.BaseType;
            }

            return fields;
        }

    }
}