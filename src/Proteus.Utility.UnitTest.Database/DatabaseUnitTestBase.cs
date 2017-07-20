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
using System.Data;

namespace Proteus.Utility.UnitTest.Database
{

    /// <summary>
    /// Standard base class for all unit test fixtures with database support
    /// </summary>
    public abstract class DatabaseUnitTestBase : UnitTestBase
    {
        /// <summary>
        /// Type of database to interact with during the test.
        /// </summary>
        protected enum DatabaseClientType
        {
            /// <summary>
            /// Microsoft SQL Server database
            /// </summary>
            SqlClient,

            /// <summary>
            /// Microsoft OleDB-compatible database
            /// </summary>
            OleDBClient,

            /// <summary>
            /// Microsoft SQL Server CE database
            /// </summary>
            SqlCeClient,

            /// <summary>
            /// SQLite database
            /// </summary>
            SqliteClient,

            /// <summary>
            /// MySQL database
            /// </summary>
            MySqlClient,

            /// <summary>
            /// Oracle database (NOT YET SUPPORTED)
            /// </summary>
            /// <remarks>
            /// Oracle database support is not yet available; as of now, Oracle interaction can be achieved via the OleDB drivers for Oracle (select OleDBClient as the database type).
            /// </remarks>
            OracleClient
        }

        /// <summary>
        /// Represents the default filepath to the XML file used to save the present state of the Database before tests begin
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\BackupData.xml" (BackupData.xml located in the \TestData\ folder directly beneath the Project folder)
        /// </remarks>
        private const string BACKUPDATAXMLFILENAME = @"TestData\BackupData.xml";

        /// <summary>
        /// Represents the default filepath to the XSD file used to represent the scope of the Database schema upon which to interact
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\Database.xsd" (Database.xsd located in the \TestData\ folder directly beneath the Project folder)
        /// </remarks>
        private const string BACKUPSCHEMAXMLFILENAME = @"TestData\Database.xsd";

        /// <summary>
        /// Represents the default filepath to the XML file that contains the test data to load into the database to support the tests
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\TestData.xml" (TestData.xml located in the \TestData\ folder directly beneath the Project folder)
        /// </remarks>
        private const string TESTDATAXMLFILENAME = @"TestData\TestData.xml";

        /// <summary>
        /// Full path and filename of XML file used to persist the existing (pre-test) content of the database to disk before tests are run.
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\BackupData.xml"
        /// </remarks>
        protected string _backupDataFilename = BACKUPDATAXMLFILENAME;

        /// <summary>
        /// Full path and filename of XSD file that represents the scope of the database to involve in the initial (pre-test) backup.<br></br>
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\Database.xsd"
        /// </remarks>
        protected string _backupSchemaFilename = BACKUPSCHEMAXMLFILENAME;

        /// <summary>
        /// Type of Database to interact with during the test.
        /// </summary>
        /// <remarks>
        /// Defaults to SQL Server.
        /// </remarks>
        protected DatabaseClientType _databaseClientType = DatabaseClientType.SqlClient;

        /// <summary>
        /// Connection string for test database.
        /// </summary>
        protected string _databaseConnectionString;

        /// <summary>
        /// Main NDbUnit database object responsible for interacting with the database.
        /// </summary>
        protected NDbUnit.Core.INDbUnitTest _database;

        /// <summary>
        /// Name for the Named Connection String in the .config file
        /// </summary>
        /// <remarks>
        /// Defaults to "testDatabase" (note that values in .config files ARE case-sensitive).
        /// </remarks>
        protected string _namedConnectionStringName = "testDatabase";

        /// <summary>
        /// Determines whether System.Diagnostics.Trace output will be provided; useful when troubleshooting behavior during test runs.
        /// </summary>
        /// <remarks>
        /// Defaults to false (no output provided).
        /// </remarks>
        protected internal bool _printTraceOutput = false;

        /// <summary>
        /// Full path and filename of XML file containing the test data.
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\TestData.xml"
        /// </remarks>
        protected string _testDataFilename = TESTDATAXMLFILENAME;

        /// <summary>
        /// Full path and filename of XSD file that represents the scope of the database to involve in the testing.
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\Database.xsd"
        /// </remarks>
        protected string _testSchemaFilename = BACKUPSCHEMAXMLFILENAME;

        public static string NDBUNIT_MYSQL_ASSEMBLY = "NDbUnit.MySql";

        public static string NDBUNIT_MYSQL_TYPE = "NDbUnit.Core.MySqlClient.MySqlDbUnitTest";

        public static string NDBUNIT_OLEDB_ASSEMBLY = "NDbUnit.OleDb";

        public static string NDBUNIT_OLEDB_TYPE = "NDbUnit.Core.OleDb.OleDbUnitTest";

        public static string NDBUNIT_ORACLE_ASSEMBLY = "NDbUnit.OracleClient";

        public static string NDBUNIT_ORACLE_TYPE = "NDbUnit.OracleClient.OracleClientDbUnitTest";

        public static string NDBUNIT_SQLCE_ASSEMBLY = "NDbUnit.SqlServerCe";

        public static string NDBUNIT_SQLCE_TYPE = "NDbUnit.Core.SqlServerCe.SqlCeUnitTest";

        public static string NDBUNIT_SQLCLIENT_ASSEMBLY = "NDbUnit.SqlClient";

        public static string NDBUNIT_SQLCLIENT_TYPE = "NDbUnit.Core.SqlClient.SqlDbUnitTest";

        public static string NDBUNIT_SQLLITE_ASSEMBLY = "NDbUnit.SqlLite";

        public static string NDBUNIT_SQLLITE_TYPE = "NDbUnit.Core.SqlLite.SqlLiteUnitTest";

        /// <summary>
        /// Full path and filename of XML file used to persist the existing (pre-test) content of the database to disk before tests are run.
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\BackupData.xml"
        /// </remarks>
        protected virtual string BackupDataFilename
        {
            get
            {
                return _backupDataFilename;
            }
        }

        /// <summary>
        /// Full path and filename of XSD file that represents the scope of the database to involve in the initial (pre-test) backup.
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\Database.xsd"
        /// </remarks>
        protected virtual string BackupSchemaFilename
        {
            get
            {
                return _backupSchemaFilename;
            }
        }

        /// <summary>
        /// Gets the connection string.<br></br>
        /// Default behavior reads the connection string from the app.config file based on a named connection string of "testDatabase".<br></br>
        /// If this behavior is inappropriate, the developer must either override this property accessor when deriving from the base class<br></br>
        /// (if complex behavior override is desired) or simply set the _databaseConnectionString field variable directly.
        /// </summary>
        /// <value>The database connection string.</value>
        /// <remarks>
        /// For default behavior to take place, ensure the following section is present in app.config:<br></br>
        /// <para>
        /// &lt;configuration&gt;<br></br>
        /// &lt;connectionStrings&gt;<br></br>
        /// &lt;add name="testDatabase" connectionString="data source=TestDatabaseServer;initial catalog=test;user id=TestUser;password=Password;"/&gt;<br></br>
        /// &lt;/connectionStrings&gt;<br></br>
        /// &lt;/configuration&gt;
        /// </para>
        /// </remarks>
        protected virtual string DatabaseConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_databaseConnectionString))
                {
                    try
                    {
                        _databaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[_namedConnectionStringName].ConnectionString;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(
                            $"Unable to read named connection string '{_namedConnectionStringName}' from .config file; ensure this named connection string is provided or override the DatabaseConnectionString property or the _databaseConnectionString field value directly to set the connection string properly.", ex);
                    }
                }

                return _databaseConnectionString;
            }
        }

        /// <summary>
        /// Full path and filename of XML file containing the test data.
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\TestData.xml"
        /// </remarks>
        protected virtual string TestDataFilename
        {
            get
            {
                return _testDataFilename;
            }
        }

        /// <summary>
        /// Full path and filename of XSD file that represents the scope of the database to involve in the testing.
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\Database.xsd"
        /// </remarks>
        protected virtual string TestSchemaFilename
        {
            get
            {
                return _testSchemaFilename;
            }
        }

        /// <summary>
        /// Performs initial database setup tasks.  Intended to be invoked from within the [TestTestFixtureSetUp]-attributed method.
        /// </summary>
        protected virtual void DatabaseTestFixtureSetUp()
        {
            DatabaseTestFixtureSetUp(false);
        }

        /// <summary>
        /// Performs initial database setup tasks.  Intended to be invoked from within the [TestTestFixtureSetUp]-attributed method.
        /// </summary>
        /// <param name="ignoreSchemaDifferences">if set to <c>true</c> [ignore schema differences].</param>
        protected virtual void DatabaseTestFixtureSetUp(bool ignoreSchemaDifferences)
        {

            if (!ignoreSchemaDifferences)
            {
                //if the same schema file isn't in use for both test and backup, validate the test schema file...
                if (BackupSchemaFilename != TestSchemaFilename)
                    ValidateSchemaAgainstDatabase(TestSchemaFilename);

                //no matter what, also validate the backup schema file...
                ValidateSchemaAgainstDatabase(BackupSchemaFilename);
            }

            SaveBackupDatabase();
            OutputTraceMessage("DatabaseUnitTestBase: DatabaseTestFixtureSetUp Complete");

        }

        /// <summary>
        /// Returns database to original state after all tests are run.  Intended to be invoked from within the [TestFixtureTearDown]-attributed method.
        /// </summary>
        protected virtual void DatabaseTestFixtureTearDown()
        {
            LoadBackupDatabase();
            OutputTraceMessage("DatabaseUnitTestBase: DatabaseTestFixtureTearDown Complete");
        }

        /// <summary>
        /// Prepares the database as needed before each test is run.  Intended to be invoked from within the [SetUp]-attributed method.
        /// </summary>
        protected virtual void DatabaseSetUp()
        {
            LoadTestDatabase();
            OutputTraceMessage("DatabaseUnitTestBase: DatabaseSetUp Complete");
        }

        /// <summary>
        /// Cleans up the database after each test is run.  Intended to be invoked from within the [TearDown]-attributed method.
        /// </summary>
        protected virtual void DatabaseTearDown()
        {
            OutputTraceMessage("DatabaseUnitTestBase: DatabaseTearDown Complete");
        }

        /// <summary>
        /// Loads the backup database.
        /// </summary>
        /// <remarks>
        /// Convenience method that passes calls to LoadDatabase() method with predetermined parameters.
        /// </remarks>
        protected virtual void LoadBackupDatabase()
        {
            LoadDatabase(DatabaseConnectionString, BackupSchemaFilename, BackupDataFilename, _databaseClientType);
        }

        /// <summary>
        /// Loads the database with existing data.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="schemaFilePathName">Name of the schema file path.</param>
        /// <param name="datasetFilePathName">Name of the dataset file path.</param>
        /// <param name="clientType">Type of the client.</param>
        /// <remarks>Loads all data from the datasetFilePathName .XML file into all tables contained in the schemaFilePathName .XSD file.</remarks>
        protected virtual void LoadDatabase(string connectionString, string schemaFilePathName, string datasetFilePathName, DatabaseClientType clientType)
        {
            ValidateConnectionString(connectionString);
            ValidateSupportFileExists(schemaFilePathName);
            ValidateSupportFileExists(datasetFilePathName);

            _database = GetDatabase(connectionString, clientType);

            _database.ReadXmlSchema(schemaFilePathName);
            _database.ReadXml(datasetFilePathName);

            _database.PerformDbOperation(NDbUnit.Core.DbOperationFlag.CleanInsertIdentity);
        }

        /// <summary>
        /// Loads the test database.
        /// </summary>
        /// <remarks>
        /// Convenience method that passes calls to LoadDatabase method with predetermined parameters.
        /// </remarks>
        protected virtual void LoadTestDatabase()
        {
            LoadDatabase(DatabaseConnectionString, TestSchemaFilename, TestDataFilename, _databaseClientType);
        }

        /// <summary>
        /// Resets the database.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="clientType">Type of the database.</param>
        protected virtual void ResetDatabase(string connectionString, DatabaseClientType clientType)
        {
            _database = GetDatabase(connectionString, clientType);
            _database.PerformDbOperation(NDbUnit.Core.DbOperationFlag.Refresh);
        }

        /// <summary>
        /// Saves the backup database.
        /// </summary>
        /// <remarks>
        /// Convenience method that cascades through to calling SaveDatabase() methods with preset parameters.
        /// </remarks>
        protected virtual void SaveBackupDatabase()
        {
            SaveDatabase(DatabaseConnectionString, BackupSchemaFilename, BackupDataFilename, _databaseClientType);
        }

        /// <summary>
        /// Saves the existing database.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="schemaFilePathName">Name of the schema file path.</param>
        /// <param name="datasetFilePathName">Name of the dataset file path.</param>
        /// <param name="clientType">Type of the client.</param>
        protected virtual void SaveDatabase(string connectionString, string schemaFilePathName, string datasetFilePathName, DatabaseClientType clientType)
        {
            ValidateConnectionString(connectionString);
            ValidateSupportFileExists(schemaFilePathName);
            ValidateSupportDirectoryExists(datasetFilePathName);

            _database = GetDatabase(connectionString, clientType);

            _database.ReadXmlSchema(schemaFilePathName);

            using (DataSet ds = _database.GetDataSetFromDb())
            {
                ds.WriteXml(datasetFilePathName);
            }
        }

        /// <summary>
        /// Saves the test database.
        /// </summary>
        /// <remarks>
        /// Convenience method that cascades through to calling SaveDatabase() methods with preset parameters.
        /// </remarks>
        protected virtual void SaveTestDatabase()
        {
            SaveDatabase(DatabaseConnectionString, TestSchemaFilename, TestDataFilename, _databaseClientType);
        }

        /// <summary>
        /// Validates the schema against the database and throws exceptions if differences exist.
        /// </summary>
        /// <param name="schemaFilename">The schema filename.</param>
        /// <exception cref="System.InvalidOperationException">Differences exist between the schema file and the database that must be resolved before running test(s).
        /// </exception>
        protected void ValidateSchemaAgainstDatabase(string schemaFilename)
        {
            ValidateConnectionString(DatabaseConnectionString);
            ValidateSupportFileExists(schemaFilename);

            ISchemaValidator validator;

            switch (_databaseClientType)
            {
                case DatabaseClientType.SqlClient:
                    validator = new SqlClientSchemaValidator(schemaFilename, DatabaseConnectionString);
                    break;

                case DatabaseClientType.SqliteClient:
                case DatabaseClientType.OleDBClient:
                case DatabaseClientType.MySqlClient:
                case DatabaseClientType.SqlCeClient:
                case DatabaseClientType.OracleClient:
                    validator = new NullSchemaValidator();
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Unsupported Database client type: {_databaseClientType.ToString()}");
            }

            SchemaComparisonReport compareResults = validator.Validate();

            foreach (string errorReport in compareResults.Errors)
            {
                System.Diagnostics.Debug.WriteLine($"SCHEMA_VALIDATION_ERROR ({schemaFilename}): {errorReport}");
            }

            foreach (string warnReport in compareResults.Warnings)
            {
                System.Diagnostics.Debug.WriteLine($"SCHEMA_VALIDATION_WARNING ({schemaFilename}): {warnReport}");
            }
            foreach (string infoReport in compareResults.Information)
            {
                System.Diagnostics.Debug.WriteLine($"SCHEMA_VALIDATION_INFO ({schemaFilename}): {infoReport}");
            }

            if (compareResults.Errors.Count > 0)
                throw new InvalidOperationException(
                    $"Cannot validate XSD Schema file {schemaFilename} against existing database schema.\nTests cannot continue else data may be lost.\nCorrect errors in schema file and attempt tests again.");

        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="clientType">Type of the client.</param>
        /// <returns>NDbUnit database object to interact with the Database</returns>
        private NDbUnit.Core.INDbUnitTest GetDatabase(string connectionString, DatabaseClientType clientType)
        {
            switch (clientType)
            {
                case DatabaseClientType.SqlClient:
                    return DatabaseSpecificTypeFactory.CreateINDBUnitTest(NDBUNIT_SQLCLIENT_TYPE, NDBUNIT_SQLCLIENT_ASSEMBLY, connectionString);

                case DatabaseClientType.OleDBClient:
                    return DatabaseSpecificTypeFactory.CreateINDBUnitTest(NDBUNIT_OLEDB_TYPE, NDBUNIT_OLEDB_ASSEMBLY, connectionString);

                case DatabaseClientType.SqlCeClient:
                    return DatabaseSpecificTypeFactory.CreateINDBUnitTest(NDBUNIT_SQLCE_TYPE, NDBUNIT_SQLCE_ASSEMBLY, connectionString);

                case DatabaseClientType.SqliteClient:
                    return DatabaseSpecificTypeFactory.CreateINDBUnitTest(NDBUNIT_SQLLITE_TYPE, NDBUNIT_SQLLITE_ASSEMBLY, connectionString);

                case DatabaseClientType.MySqlClient:
                    return DatabaseSpecificTypeFactory.CreateINDBUnitTest(NDBUNIT_MYSQL_TYPE, NDBUNIT_MYSQL_ASSEMBLY, connectionString);

                case DatabaseClientType.OracleClient:
                    return DatabaseSpecificTypeFactory.CreateINDBUnitTest(NDBUNIT_ORACLE_TYPE, NDBUNIT_ORACLE_ASSEMBLY, connectionString);

                default:
                    throw new InvalidOperationException($"Unsupported DatabaseClientType: {clientType.ToString()}");
            }

        }

        /// <summary>
        /// Outputs the trace message depending on the value of _printTraceOutput.
        /// </summary>
        /// <param name="msg">The message.</param>
        private void OutputTraceMessage(string msg)
        {
            if (_printTraceOutput)
                System.Diagnostics.Trace.WriteLine(msg);
        }

        /// <summary>
        /// Validates the connection string is not empty.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        private static void ValidateConnectionString(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("_databaseConnectionString field cannot be null or empty!\nSet a value for _databaseConnectionString\nor override the DatabaseConnectionString property from the base class to provide a custom getter for this value.");
        }

        /// <summary>
        /// Validates the support directory exists.
        /// </summary>
        /// <param name="filePathName">Name of the file path.</param>
        private void ValidateSupportDirectoryExists(string filePathName)
        {
            if (string.IsNullOrEmpty(filePathName))
            {
                throw new InvalidOperationException("XSD and/or XML file paths cannot be empty or null!");
            }

            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePathName)))
            {
                throw new System.IO.IOException($@"Folder ""{
                        System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(filePathName))
                    }"" not found or inaccessible!");
            }
        }

        /// <summary>
        /// Validates the schema file exists.
        /// </summary>
        /// <param name="filePathName">Name of the schema file path.</param>
        private static void ValidateSupportFileExists(string filePathName)
        {
            if (string.IsNullOrEmpty(filePathName))
            {
                throw new InvalidOperationException("XSD and/or XML file paths cannot be empty or null!");
            }

            if (!System.IO.File.Exists(filePathName))
            {
                throw new System.IO.IOException($@"Unable to load XSD/XML file!  File ""{
                        System.IO.Path.GetFullPath(filePathName)
                    }"" not found or inaccessible!");
            }
        }

        /// <summary>
        /// Helper class to assist in the creation of Guids with specific values.
        /// </summary>
        protected static class GuidGen
        {
            /// <summary>
            /// Creates a new Guid constructed by repeating the specified character.
            /// </summary>
            /// <param name="characterToRepeat">The character to repeat for the Guid.</param>
            /// <returns>A new Guid constructed from the provided character.</returns>
            /// <example>GuidGen.Repeat("1") returns a new Guid of the form 1111-1111-1111-111111111111-11111111</example>
            public static Guid Repeat(object characterToRepeat)
            {
                if (characterToRepeat == null)
                    throw new ArgumentNullException("characterToRepeat", "Cannot be null.");

                var characterToRepeatString = characterToRepeat.ToString();

                characterToRepeatString = characterToRepeatString.Trim();

                if (characterToRepeatString.Length != 1)
                    throw new ArgumentOutOfRangeException("characterToRepeat", "Length must be exactly one character.");

                try
                {
                    return new Guid(string.Format("{0}{0}{0}{0}{0}{0}{0}{0}-{0}{0}{0}{0}-{0}{0}{0}{0}-{0}{0}{0}{0}-{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}", characterToRepeat));
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message, "characterToRepeat", ex);
                }
            }

        }

    }
}
