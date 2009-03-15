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
using System.Text;
using System.Data;
using System.Reflection;

namespace Proteus.Utility.UnitTest
{
    /// <summary>
    /// Standard base class for all unit test fixtures
    /// </summary>
    public abstract class UnitTestBase
    {
        /// <summary>
        /// Convenience method that uses reflection to return the value of a non-public field of a given object.
        /// </summary>
        /// <remarks>Useful in certain instances during testing to avoid the need to add protected properties, etc. to a class just to facilitate testing.</remarks>
        /// <param name="obj">The instance of the object from which to retrieve the field value.</param>
        /// <param name="fieldName">Name of the field on the object from which to retrieve the value.</param>
        /// <returns></returns>
        protected virtual object GetInstanceFieldValue(object obj, string fieldName)
        {
            if (obj == null)
                throw new ArgumentNullException("obj", "obj is null.");
            if (String.IsNullOrEmpty(fieldName))
                throw new ArgumentException("fieldName is null or empty.", "fieldName");

            FieldInfo f = obj.GetType().GetField(fieldName, BindingFlags.SetField | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (f != null)
                return f.GetValue(obj);
            else
            {
                throw new ArgumentException(string.Format("Non-public instance field '{0}' could not be found in class of type '{1}'", fieldName, obj.GetType().ToString()));
            }
        }

        /// <summary>
        /// Convenience method that uses reflection to set the value of a non-public field of a given object.
        /// </summary>
        /// <remarks>Useful in certain instances during testing to avoid the need to add protected properties, etc. to a class just to facilitate testing.</remarks>
        /// <param name="obj">The instance of the object from which to set the field value.</param>
        /// <param name="fieldName">Name of the field on the object to which to set the value.</param>
        /// <param name="fieldValue">The field value to set.</param>
        protected virtual void SetInstanceFieldValue(object obj, string fieldName, object fieldValue)
        {

            if (obj == null)
                throw new ArgumentNullException("obj", "obj is null.");
            if (String.IsNullOrEmpty(fieldName))
                throw new ArgumentException("fieldName is null or empty.", "fieldName");
            if (fieldValue == null)
                throw new ArgumentNullException("fieldValue", "fieldValue is null.");

            FieldInfo f = obj.GetType().GetField(fieldName, BindingFlags.SetField | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (f != null)
            {
                if (f.FieldType != fieldValue.GetType())
                    throw new ArgumentException(string.Format("fieldValue for fieldName '{0}' of object type '{1}' must be of type '{2}' but was of type '{3}'", fieldName, obj.GetType().ToString(), f.FieldType.ToString(), fieldValue.GetType().ToString()), "fieldValue");

                f.SetValue(obj, fieldValue);
            }
            else
            {
                throw new ArgumentException(string.Format("Non-public instance field '{0}' could not be found in class of type '{1}'", fieldName, obj.GetType().ToString()));
            }
        }

        /// <summary>
        /// Static String values for [Category(...)] attribute to ensure consistency across all TestFixtures
        /// </summary>
        public static class UnitTestType
        {
            /// <summary>
            /// All unit tests with dependencies on interacting with a database
            /// </summary>
            public const string DatabaseDependent = "DatabaseDependent";

            /// <summary>
            /// All unit tests with dependencies on other external applications (e.g. thru COM automation, etc.)
            /// </summary>
            public const string ExternalApplicationDependent = "ExternalApplicationDependent";

            /// <summary>
            /// All unit tests that interact with any other external dependency
            /// </summary>
            public const string Integration = "Integration";

            /// <summary>
            /// All 'pure' unit tests that do not have any extenal dependencies (intended to be the most common value used)
            /// </summary>
            public const string Unit = "Unit";

            /// <summary>
            /// All unit tests that exercise the user-interface aspects of the code (e.g., WatiN, etc.)
            /// </summary>
            public const string UserInterface = "UserInterface";

        }
    }
}
