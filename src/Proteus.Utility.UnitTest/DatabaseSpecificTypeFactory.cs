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
