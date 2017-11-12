#region License

/*
 * Copyright © 2009-2017 the original author or authors.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
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