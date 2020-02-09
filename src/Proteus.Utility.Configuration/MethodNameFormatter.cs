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
using System.Linq;
using System.Reflection;

namespace Proteus.Utility.Configuration
{
    public static class MethodNameFormatter
    {
        public static string GetFormattedName(Delegate @delegate)
        {
            var methodName = @delegate.ToString();

            var method = @delegate.Method;

            if (method.DeclaringType != null)
            {
                methodName = GetFullTypeName(method) + "." + GetMethodName(method) + GetArguments(method);
            }

            return methodName;
        }

        private static string GetMethodName(MethodInfo method)
        {
            return method.Name;
        }

        private static string GetFullTypeName(MethodInfo method)
        {
            return method.DeclaringType?.FullName ?? "DYNAMIC_TYPE";
        }

        private static string GetArguments(MethodInfo method)
        {
            return "(" + string.Join(", ", method.GetParameters().Select(arg => arg.Name).ToArray()) + ")";
        }
    }
}