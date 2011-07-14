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
using System.Text;
using System.Reflection;
using NDbUnit.Core;
using System.Data;

namespace Proteus.Utility.UnitTest
{
    public sealed class DatabaseSpecificTypeFactory
    {
        private DatabaseSpecificTypeFactory() { }


        public static INDbUnitTest CreateINDBUnitTest(string NDbUnitClassName, string NDbUnitAssemblyName, string connectionString)
        {
            return CreateObjectWithOneStringConstructor<INDbUnitTest>(NDbUnitClassName, NDbUnitAssemblyName, connectionString);
        }

        private static T CreateObjectWithTwoStringConstructor<T>(string className, string assemblyName, string constructorArg) where T : class
        {
            Type type = ResolveType(className, assemblyName);
            if (type == null)
                throw new ArgumentException(String.Format("Can't load type {0}", className));


            Type[] types = new Type[] { typeof(string), typeof(string) };
            ConstructorInfo info = type.GetConstructor(types);

            if (info == null)
                throw new ArgumentException(String.Format("Can't find matching constructor (string, string) for type {0}", className));

            object targetType = info.Invoke(new object[] { string.Empty, constructorArg });
            if (targetType == null)
                throw new ArgumentException(String.Format("Can't instantiate type {0}", className));

            return targetType as T;
        }

        private static T CreateObjectWithOneStringConstructor<T>(string className, string assemblyName, string constructorArg) where T : class
        {
            Type type = ResolveType(className, assemblyName);
            if (type == null)
                throw new ArgumentException(String.Format("Can't load type {0}", className));


            Type[] types = new Type[] { typeof(string) };
            ConstructorInfo info = type.GetConstructor(types);

            if (info == null)
                throw new ArgumentException(String.Format("Can't find matching constructor (string, string) for type {0}", className));

            object targetType = info.Invoke(new object[] { constructorArg });
            if (targetType == null)
                throw new ArgumentException(String.Format("Can't instantiate type {0}", className));

            return targetType as T;
        }

        public static IDbConnection CreateIDbConnection(string DbConnectionClassName, string DbConnectionAssemblyName, string connectionString)
        {
            return CreateObjectWithOneStringConstructor<IDbConnection>(DbConnectionClassName, DbConnectionAssemblyName, connectionString);
        }


        public static IDbDataAdapter CreateIDbDataAdapter(string DbDataAdapterClassName, string DbDataAdapterAssemblyName, string connectionString)
        {
            return CreateObjectWithTwoStringConstructor<IDbDataAdapter>(DbDataAdapterClassName, DbDataAdapterAssemblyName, connectionString);
        }

        private static Type ResolveType(string className, string assemblyName)
        {
            Assembly assembly;

            try
            {
                assembly = Assembly.Load(assemblyName);
            }
            catch
            {
                throw new ArgumentException(String.Format("Can't load assembly {0}; ensure the assembly is located in the app binary directory or the GAC", assemblyName));
            }

            return assembly.GetType(className, false, false);
        }
    }
}
