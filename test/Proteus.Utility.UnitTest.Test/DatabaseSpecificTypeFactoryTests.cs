/*
 *
 * Proteus
 * Copyright (c) 2008 - 2011
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
using NUnit.Framework;
using Proteus.Utility.UnitTest;
using System.IO;
using Proteus.Utility.UnitTest.Database;

namespace Proteus.Utility.UnitTest.Test
{
    [TestFixture]
    public class DatabaseSpecificTypeFactoryTests
    {
        private IEnumerable<string> _providerAssemblyNames = new List<string>() { "NDbUnit.MySql.dll", "NDbUnit.OleDb.dll", "NDbUnit.OracleClient.dll", "NDbUnit.SqlClient.dll", "NDbUnit.SqlLite.dll", "NDbUnit.SqlServerCe.dll" };

        private string CONN_STRING = string.Empty;

        [TestFixtureSetUp]
        public void _TestTestFixtureSetUp()
        {
            //have to ensure that all NDbUnit.* files are properly located in the app-local folder in order to be dynamically loaded when needed
            //...since they (by design) aren't attached as binary references, VS won't copy them to the output folder for us!
            foreach (string filename in _providerAssemblyNames)
            {
                File.Copy($@"..\..\Lib\{filename}", $@".\{filename}", true);
            }

        }

        [Test]
        public void CanInstantiateNDbUnitTestObject_With_MySql()
        {
            var actual = DatabaseSpecificTypeFactory.CreateINDBUnitTest(DatabaseUnitTestBase.NDBUNIT_MYSQL_TYPE, DatabaseUnitTestBase.NDBUNIT_MYSQL_ASSEMBLY, CONN_STRING);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void CanInstantiateNDbUnitTestObject_With_OleDb()
        {
            var actual = DatabaseSpecificTypeFactory.CreateINDBUnitTest(DatabaseUnitTestBase.NDBUNIT_OLEDB_TYPE, DatabaseUnitTestBase.NDBUNIT_OLEDB_ASSEMBLY, CONN_STRING);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void CanInstantiateNDbUnitTestObject_With_Oracle()
        {
            var actual = DatabaseSpecificTypeFactory.CreateINDBUnitTest(DatabaseUnitTestBase.NDBUNIT_ORACLE_TYPE, DatabaseUnitTestBase.NDBUNIT_ORACLE_ASSEMBLY, CONN_STRING);
            Assert.IsNotNull(actual);
        }

        public void CanInstantiateNDbUnitTestObject_With_SqlLite()
        {
            var actual = DatabaseSpecificTypeFactory.CreateINDBUnitTest(DatabaseUnitTestBase.NDBUNIT_SQLLITE_TYPE, DatabaseUnitTestBase.NDBUNIT_SQLLITE_ASSEMBLY, CONN_STRING);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void CanInstantiateNDbUnitTestObject_With_SqlServerCe()
        {
            var actual = DatabaseSpecificTypeFactory.CreateINDBUnitTest(DatabaseUnitTestBase.NDBUNIT_SQLCE_TYPE, DatabaseUnitTestBase.NDBUNIT_SQLCE_ASSEMBLY, CONN_STRING);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void CanInstantiateNDbUnitTestObject_With_SqlServer()
        {
            var actual = DatabaseSpecificTypeFactory.CreateINDBUnitTest(DatabaseUnitTestBase.NDBUNIT_SQLCLIENT_TYPE, DatabaseUnitTestBase.NDBUNIT_SQLCLIENT_ASSEMBLY, CONN_STRING);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void CanInstantiateSqlDataAdapterObject()
        {
            var actual = DatabaseSpecificTypeFactory.CreateIDbDataAdapter("System.Data.SqlClient.SqlDataAdapter", "System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", CONN_STRING);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void CanThrowErrorWhenAssemblyNotFound()
        {
            Assert.Throws<ArgumentException>(() => DatabaseSpecificTypeFactory.CreateINDBUnitTest(DatabaseUnitTestBase.NDBUNIT_SQLCLIENT_TYPE, "XYZ", CONN_STRING));
        }

        [Test]
        public void CanThrowWhenAssemblyFoundButCantFindType()
        {
            Assert.Throws<ArgumentException>(() => DatabaseSpecificTypeFactory.CreateINDBUnitTest("ZZZ", DatabaseUnitTestBase.NDBUNIT_SQLCLIENT_ASSEMBLY, CONN_STRING));
        }

        [Test]
        public void CanThrowWhenTypeAndAssemblyFoundButConstructorSignatureFailsToMatch()
        {
            Assert.Throws<ArgumentException>(() => DatabaseSpecificTypeFactory.CreateIDbDataAdapter("System.Data.SqlClient.SqlClientPermission", "System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", CONN_STRING));
        }

    }

}
