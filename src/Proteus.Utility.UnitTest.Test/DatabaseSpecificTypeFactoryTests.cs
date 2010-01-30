/*
 *
 * Proteus
 * Copyright (C) 2008, 2009, 2010
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
using MbUnit.Framework;
using Proteus.Utility.UnitTest;
using System.IO;

namespace Proteus.Utility.UnitTest.Test
{
    [TestFixture]
    public class DatabaseSpecificTypeFactoryTests
    {
        private IEnumerable<string> _providerAssemblyNames = new List<string>() { "NDbUnit.MySql.dll", "NDbUnit.OleDb.dll", "NDbUnit.OracleClient.dll", "NDbUnit.SqlClient.dll", "NDbUnit.SqlLite.dll", "NDbUnit.SqlServerCe.dll" };

        private string CONN_STRING = string.Empty;

        [FixtureSetUp]
        public void _TestFixtureSetUp()
        {
            //have to ensure that all NDbUnit.* files are properly located in the app-local folder in order to be dynamically loaded when needed
            //...since they (by design) aren't attached as binary references, VS won't copy them to the output folder for us!
            foreach (string filename in _providerAssemblyNames)
            {
                File.Copy(string.Format(@"..\..\..\..\Lib\{0}", filename), string.Format(@".\{0}", filename), true);
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
