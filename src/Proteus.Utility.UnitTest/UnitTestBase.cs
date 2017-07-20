/*
 *
 * Proteus
 * Copyright (c) 2008 - 2017
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
                throw new ArgumentException(
                    $"Non-public instance field '{fieldName}' could not be found in class of type '{obj.GetType().ToString()}'");
            }
        }

        /// <summary>
        /// Gets the instance field value.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>TResult.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        protected virtual TResult GetInstanceFieldValue<TResult>(object obj, string fieldName)
        {
            var objectResult = GetInstanceFieldValue(obj, fieldName);

            TResult result;

            try
            {
                result = (TResult)objectResult;
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException(
                    $"Non-public instance field '{fieldName}' in object of type '{obj.GetType().ToString()}' is not expected type of '{typeof(TResult)}'");
            }

            return result;
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
                    throw new ArgumentException(
                        $"fieldValue for fieldName '{fieldName}' of object type '{obj.GetType().ToString()}' must be of type '{f.FieldType.ToString()}' but was of type '{fieldValue.GetType().ToString()}'", "fieldValue");

                f.SetValue(obj, fieldValue);
            }
            else
            {
                throw new ArgumentException(
                    $"Non-public instance field '{fieldName}' could not be found in class of type '{obj.GetType().ToString()}'");
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
