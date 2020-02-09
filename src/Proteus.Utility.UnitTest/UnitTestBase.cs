#region License

/*
*
*  Copyright (c) 2020 Stephen A. Bohlen
*
*  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
*  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
*  and/or sell copies of the Software, and to permit persons to whom the Software is *furnished to do so, subject to the following conditions:
* 
*  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
*  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
*  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
*  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
*  IN THE SOFTWARE.
*
*/

#endregion




using System;
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
                throw new ArgumentNullException(nameof(obj), "obj is null.");
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("fieldName is null or empty.", nameof(fieldName));

            FieldInfo f = obj.GetType().GetField(fieldName, BindingFlags.SetField | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (f != null)
                return f.GetValue(obj);
            else
            {
                throw new ArgumentException(
                    $"Non-public instance field '{fieldName}' could not be found in class of type '{obj.GetType()}'");
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
                    $"Non-public instance field '{fieldName}' in object of type '{obj.GetType()}' is not expected type of '{typeof(TResult)}'");
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
                throw new ArgumentNullException(nameof(obj), "obj is null.");
            if (String.IsNullOrEmpty(fieldName))
                throw new ArgumentException("fieldName is null or empty.", nameof(fieldName));
            
            FieldInfo f = obj.GetType().GetField(fieldName, BindingFlags.SetField | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (f != null)
            {
                if (f.FieldType != fieldValue.GetType())
                    throw new ArgumentException(
                        $"fieldValue for fieldName '{fieldName}' of object type '{obj.GetType()}' must be of type '{f.FieldType}' but was of type '{fieldValue.GetType()}'", nameof(fieldValue));

                f.SetValue(obj, fieldValue);
            }
            else
            {
                throw new ArgumentException(
                    $"Non-public instance field '{fieldName}' could not be found in class of type '{obj.GetType()}'");
            }
        }
    }
}
