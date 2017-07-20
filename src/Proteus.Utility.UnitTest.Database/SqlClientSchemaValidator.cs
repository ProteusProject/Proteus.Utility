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
using System.Data;
using System.Data.SqlClient;

namespace Proteus.Utility.UnitTest.Database
{

    public class SqlClientSchemaValidator : ISchemaValidator
    {
        private string _connectionString;

        private string _schemaFilename;

        /// <summary>
        /// Initializes a new instance of the SqlClientSchemaValidator class.
        /// </summary>
        /// <param name="schemaFilename"></param>
        /// <param name="connectionString"></param>
        public SqlClientSchemaValidator(string schemaFilename, string connectionString)
        {
            if (String.IsNullOrEmpty(schemaFilename))
                throw new ArgumentException("schemaFilename is null or empty.", "schemaFilename");
            if (String.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connectionString is null or empty.", "connectionString");

            _schemaFilename = schemaFilename;
            _connectionString = connectionString;
        }


        public SchemaComparisonReport Validate()
        {

            using (DataSet dsSchema = new DataSet())
            {
                SchemaComparisonReport compareResults = new SchemaComparisonReport();

                dsSchema.ReadXmlSchema(_schemaFilename);

                var encoder = new TableNameEncoder();

                string tblName;
                string colName;
                string assemblyName;

                IDbConnection databaseConnection;
                IDbDataAdapter dataAdapter;

                assemblyName = "System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
                databaseConnection = DatabaseSpecificTypeFactory.CreateIDbConnection("System.Data.SqlClient.SqlConnection", assemblyName, _connectionString);
                dataAdapter = DatabaseSpecificTypeFactory.CreateIDbDataAdapter("System.Data.SqlClient.SqlDataAdapter", assemblyName, _connectionString); ;

                using (DataSet dsDB = new DataSet())
                {
                    DataTable dtDB;
                    DataColumn dcDB;
                    foreach (DataTable dtSchema in dsSchema.Tables)
                    {
                        tblName = encoder.Encode(dtSchema.TableName);
                        dataAdapter.SelectCommand.CommandText = $"select top 1 * from {tblName}";
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
                                if (dcDB == null)
                                    // does the column exist in the database
                                    compareResults.Errors.Add(
                                        $"{tblName}.{colName}  :  Column does not exist in database.");
                                else
                                    if (dcSchema1.DataType != dcDB.DataType)
                                        // do the datatypes match
                                        compareResults.Errors.Add(
                                            $"{tblName}.{colName}  :  Column's datatype does not match (Db = {dcDB.DataType.FullName},  Xsd = {dcSchema1.DataType.FullName})");
                                    else
                                        if (dcSchema1.DataType == typeof(string) && dcSchema1.MaxLength != dcDB.MaxLength)
                                            // if the column is a string, compare the maximum length
                                            compareResults.Errors.Add(
                                                $"{tblName}.{colName}  : Column of type String has different maximum length (Db = {dcDB.MaxLength},  Xsd = {dcSchema1.MaxLength})");
                                // do we want to compare: AutoIncrement, AutoIncrementSeed, AutoIncrementStep?
                            }
                            // determine if new columns have been added to database
                            DataColumn dcSchema2;
                            foreach (DataColumn dcDB2 in dtDB.Columns)
                            {
                                colName = dcDB2.ColumnName;
                                dcSchema2 = dtSchema.Columns[colName];
                                if (dcSchema2 == null)
                                    if (dcDB2.AllowDBNull == false)
                                    {
                                        if (dcDB2.AutoIncrement == true)
                                            compareResults.Information.Add(
                                                $"{tblName}.{colName}  :  Column does not exist in test schema and Db column does not allow null values, but should be okay because Db column is set to auto increment (Identity).");
                                        else
                                            /*If we've gotten here, there is no way to determine what will happen if the test data is inserted into the database because there is not enough information to determine
                                              whether the database insert command will fail due to null values.
                                              The database column contains a DefaultValue property but it is misleading because it does not reflect the actual default value in the database.
                                              The only accurate way to determine the default value of a database column is to query the INFORMATION_SCHEMA which only works with MS SQL Server.
                                              Since we are trying to make this database agnostic, we need to avoid querying the INFORMATION_SCHEMA.
                                            */
                                            compareResults.Warnings.Add(
                                                $"{tblName}.{colName}  :  Column does not exist in test schema, Db column does not allow null values, Db Column is not set to Auto Increment (Identity), and there is no way to determine if Db column has a default value therefore SqlExceptions may occur when test data is inserted into database.");
                                    }
                                    else
                                        compareResults.Information.Add(
                                            $"{tblName}.{colName}  :  Column does not exist in test schema, but should be okay because Db column allows null values.");
                            }
                        }
                        catch (SqlException)
                        {
                            compareResults.Errors.Add($"{tblName}  :  Table does not exist in database.");
                        }
                    }
                }

                return compareResults;

            }
        }

    }
}