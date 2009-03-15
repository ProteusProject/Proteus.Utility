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
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Proteus.Utility.UnitTest
{

    /// <summary>
    /// A report containing a list of Errors, Warnings and Information that are the result of comparing a test schema to an existing database.
    /// If an error is reported then the text fixture setup should be halted and test schema should be updated to correct all errors.
    /// </summary>
    public class SchemaComparisonReport
    {

        /// <summary>
        /// Error message collection
        /// </summary>
        protected List<string> _errors;

        /// <summary>
        /// Information message collection
        /// </summary>
        protected List<string> _information;

        /// <summary>
        /// Warning message collection
        /// </summary>
        protected List<string> _warnings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaComparisonReport"/> class.
        /// </summary>
        public SchemaComparisonReport()
        {
            _errors = new List<string>();
            _warnings = new List<string>();
            _information = new List<string>();
        }

        /// <summary>
        /// Read-only collection of error-level messages returned from the SchemaComparisonReport
        /// </summary>
        public IList<string> Errors { get { return this._errors; } }

        /// <summary>
        /// Read-only collection of information-level messages returned from the SchemaComparisonReport
        /// </summary>
        public IList<string> Information { get { return this._information; } }

        /// <summary>
        /// Read-only collection of warning-level messages returned from the SchemaComparisonReport
        /// </summary>
        public IList<string> Warnings { get { return this._warnings; } }

    }

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
        private const string BACKUPDATAXMLFILENAME = @"..\..\TestData\BackupData.xml";

        /// <summary>
        /// Represents the default filepath to the XSD file used to represent the scope of the Database schema upon which to interact
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\Database.xsd" (Database.xsd located in the \TestData\ folder directly beneath the Project folder)
        /// </remarks>
        private const string BACKUPSCHEMAXMLFILENAME = @"..\..\TestData\Database.xsd";

        /// <summary>
        /// Represents the default filepath to the XML file that contains the test data to load into the database to support the tests
        /// </summary>
        /// <remarks>
        /// Defaults to "..\..\TestData\TestData.xml" (TestData.xml located in the \TestData\ folder directly beneath the Project folder)
        /// </remarks>
        private const string TESTDATAXMLFILENAME = @"..\..\TestData\TestData.xml";

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
        /// Main NDbUnit database object responsible for interacting with the database.
        /// </summary>
        protected NDbUnit.Core.INDbUnitTest _database;

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
                        throw new Exception(string.Format("Unable to read named connection string '{0}' from .config file; ensure this named connection string is provided or override the DatabaseConnectionString property or the _databaseConnectionString field value directly to set the connection string properly.", _namedConnectionStringName), ex);
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
        /// Performs initial database setup tasks.  Intended to be invoked from within the [TestFixtureSetUp]-attributed method.
        /// </summary>
        protected virtual void DatabaseFixtureSetUp()
        {
            DatabaseFixtureSetUp(false);
        }

        /// <summary>
        /// Performs initial database setup tasks.  Intended to be invoked from within the [TestFixtureSetUp]-attributed method.
        /// </summary>
        /// <param name="ignoreSchemaDifferences">if set to <c>true</c> [ignore schema differences].</param>
        protected virtual void DatabaseFixtureSetUp(bool ignoreSchemaDifferences)
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
            OutputTraceMessage("DatabaseUnitTestBase: DatabaseFixtureSetUp Complete");

        }

        /// <summary>
        /// Returns database to original state after all tests are run.  Intended to be invoked from within the [TestFixtureTearDown]-attributed method.
        /// </summary>
        protected virtual void DatabaseFixtureTearDown()
        {
            LoadBackupDatabase();
            OutputTraceMessage("DatabaseUnitTestBase: DatabaseFixtureTearDown Complete");
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

            DataSet ds = new DataSet();
            ds = _database.GetDataSetFromDb();
            ds.WriteXml(datasetFilePathName);
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
                    return new NDbUnit.Core.SqlClient.SqlDbUnitTest(connectionString);

                case DatabaseClientType.OleDBClient:
                    return new NDbUnit.Core.OleDb.OleDbUnitTest(connectionString);

                case DatabaseClientType.OracleClient:
                    throw new InvalidOperationException(string.Format("Unsupported DatabaseClientType: {0}", clientType.ToString()));

                default:
                    throw new InvalidOperationException(string.Format("Unsupported DatabaseClientType: {0}", clientType.ToString()));
            }

        }

        /// <summary>
        /// Outputs the trace message depending on the value of _printTraceOutput.
        /// </summary>
        /// <param name="msg">The message.</param>
        private void OutputTraceMessage(string msg)
        {
            if (this._printTraceOutput)
                System.Diagnostics.Trace.WriteLine(msg);
        }

        /// <summary>
        /// Validates the connection string is not empty.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        private static void ValidateConnectionString(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
                throw new System.InvalidOperationException("_databaseConnectionString field cannot be null or empty!\nSet a value for _databaseConnectionString\nor override the DatabaseConnectionString property from the base class to provide a custom getter for this value.");
        }

        /// <summary>
        /// Validates the schema against the database and throws exceptions if differences exist.
        /// </summary>
        /// <param name="schemaFilename">The schema filename.</param>
        /// <exception cref="System.InvalidOperationException">Differences exist between the schema file and the database that must be resolved before running test(s).
        /// </exception>
        private void ValidateSchemaAgainstDatabase(string schemaFilename)
        {
            ValidateConnectionString(DatabaseConnectionString);
            ValidateSupportFileExists(schemaFilename);

            SchemaComparisonReport compareResults = new SchemaComparisonReport();

            DataSet dsSchema = new DataSet();
            dsSchema.ReadXmlSchema(schemaFilename);
            string tblName;
            string colName;

            IDbConnection databaseConnection = null;
            IDbDataAdapter dataAdapter = null;

            switch (_databaseClientType)
            {
                case DatabaseClientType.SqlClient:
                    databaseConnection = new SqlConnection(DatabaseConnectionString);
                    dataAdapter = new SqlDataAdapter("", DatabaseConnectionString);
                    break;
                case DatabaseClientType.OleDBClient:
                    databaseConnection = new OleDbConnection(this.DatabaseConnectionString);
                    dataAdapter = new OleDbDataAdapter("", DatabaseConnectionString);
                    break;
                case DatabaseClientType.OracleClient:
                    throw new InvalidOperationException(string.Format("Unsupported Database client type: {0}", _databaseClientType.ToString()));

            }

            DataSet dsDB = new DataSet();
            DataTable dtDB;
            DataColumn dcDB;

            foreach (System.Data.DataTable dtSchema in dsSchema.Tables)
            {
                tblName = dtSchema.TableName;
                dataAdapter.SelectCommand.CommandText = string.Format("select top 1 * from [{0}]", tblName);

                try
                {
                    // determine if the table exists in the database
                    dataAdapter.FillSchema(dsDB, SchemaType.Source);
                    dsDB.Tables[dsDB.Tables.Count - 1].TableName = tblName;
                    dtDB = dsDB.Tables[tblName];

                    // determine if there are any differences between the columns in the xsd and the database
                    foreach (DataColumn dcSchema1 in dtSchema.Columns)
                    {
                        colName = dcSchema1.ColumnName;
                        dcDB = dtDB.Columns[colName];

                        if (dcDB == null) // does the column exist in the database
                        {
                            compareResults.Errors.Add(tblName + "." + colName + "  :  Column does not exist in database.");
                        }
                        else if (dcSchema1.DataType != dcDB.DataType) // do the datatypes match
                        {
                            compareResults.Errors.Add(tblName + "." + colName + "  :  Column's datatype does not match (Db = " + dcDB.DataType.FullName + ",  Xsd = " + dcSchema1.DataType.FullName + ")");
                        }
                        else if (dcSchema1.DataType == typeof(string) && dcSchema1.MaxLength != dcDB.MaxLength)	// if the column is a string, compare the maximum length
                        {
                            compareResults.Errors.Add(tblName + "." + colName + "  : Column of type String has different maximum length (Db = " + dcDB.MaxLength + ",  Xsd = " + dcSchema1.MaxLength + ")");
                        }
                        // do we want to compare: AutoIncrement, AutoIncrementSeed, AutoIncrementStep?
                    }

                    // determine if new columns have been added to database
                    DataColumn dcSchema2;
                    foreach (DataColumn dcDB2 in dtDB.Columns)
                    {
                        colName = dcDB2.ColumnName;
                        dcSchema2 = dtSchema.Columns[colName];

                        if (dcSchema2 == null)
                        {
                            if (dcDB2.AllowDBNull == false)
                            {
                                if (dcDB2.AutoIncrement == true)
                                {
                                    compareResults.Information.Add(tblName + "." + colName + "  :  Column does not exist in test schema and Db column does not allow null values, but should be okay because Db column is set to auto increment (Identity).");
                                }
                                else
                                {
                                    /*
                                    If we've gotten here, there is no way to determine what will happen if the test data is inserted into the database because there is not enough information to determine
                                    whether the database insert command will fail due to null values.
                                    The database column contains a DefaultValue property but it is misleading because it does not reflect the actual default value in the database.
                                    The only accurate way to determine the default value of a database column is to query the INFORMATION_SCHEMA which only works with MS SQL Server.
                                    Since we are trying to make this database agnostic, we need to avoid querying the INFORMATION_SCHEMA.
                                    */
                                    compareResults.Warnings.Add(tblName + "." + colName + "  :  Column does not exist in test schema, Db column does not allow null values, Db Column is not set to Auto Increment (Identity), and there is no way to determine if Db column has a default value therefore SqlExceptions may occur when test data is inserted into database.");
                                }
                            }
                            else
                            {
                                compareResults.Information.Add(tblName + "." + colName + "  :  Column does not exist in test schema, but should be okay because Db column allows null values.");
                            }
                        }
                    }
                }
                catch (SqlException)
                {
                    compareResults.Errors.Add(tblName + "  :  Table does not exist in database.");
                }
            }
            foreach (string errorReport in compareResults.Errors)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("SCHEMA_VALIDATION_ERROR ({0}): {1}", schemaFilename, errorReport));
            }

            foreach (string warnReport in compareResults.Warnings)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("SCHEMA_VALIDATION_WARNING ({0}): {1}", schemaFilename, warnReport));
            }
            foreach (string infoReport in compareResults.Information)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("SCHEMA_VALIDATION_INFO ({0}): {1}", schemaFilename, infoReport));
            }

            if (compareResults.Errors.Count > 0)
                throw new InvalidOperationException(string.Format("Cannot validate XSD Schema file {0} against existing database schema.\nTests cannot continue else data may be lost.\nCorrect errors in schema file and attempt tests again.", schemaFilename));

        }

        /// <summary>
        /// Validates the support directory exists.
        /// </summary>
        /// <param name="filePathName">Name of the file path.</param>
        private void ValidateSupportDirectoryExists(string filePathName)
        {
            if (string.IsNullOrEmpty(filePathName))
            {
                throw new System.InvalidOperationException("XSD and/or XML file paths cannot be empty or null!");
            }

            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePathName)))
            {
                throw new System.IO.IOException(string.Format(@"Folder ""{0}"" not found or inaccessible!", System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(filePathName))));
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
                throw new System.InvalidOperationException("XSD and/or XML file paths cannot be empty or null!");
            }

            if (!System.IO.File.Exists(filePathName))
            {
                throw new System.IO.IOException(String.Format(@"Unable to load XSD/XML file!  File ""{0}"" not found or inaccessible!", System.IO.Path.GetFullPath(filePathName)));
            }
        }

    }
}
