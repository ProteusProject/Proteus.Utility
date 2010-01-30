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
using System.Data;
using System.Data.SqlClient;

namespace Proteus.Utility.UnitTest
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
                                if (dcDB == null)
                                    // does the column exist in the database
                                    compareResults.Errors.Add(String.Format("{0}.{1}  :  Column does not exist in database.", tblName, colName));
                                else
                                    if (dcSchema1.DataType != dcDB.DataType)
                                        // do the datatypes match
                                        compareResults.Errors.Add(String.Format("{0}.{1}  :  Column's datatype does not match (Db = {2},  Xsd = {3})", tblName, colName, dcDB.DataType.FullName, dcSchema1.DataType.FullName));
                                    else
                                        if (dcSchema1.DataType == typeof(string) && dcSchema1.MaxLength != dcDB.MaxLength)
                                            // if the column is a string, compare the maximum length
                                            compareResults.Errors.Add(String.Format("{0}.{1}  : Column of type String has different maximum length (Db = {2},  Xsd = {3})", tblName, colName, dcDB.MaxLength, dcSchema1.MaxLength));
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
                                            compareResults.Information.Add(String.Format("{0}.{1}  :  Column does not exist in test schema and Db column does not allow null values, but should be okay because Db column is set to auto increment (Identity).", tblName, colName));
                                        else
                                            /*If we've gotten here, there is no way to determine what will happen if the test data is inserted into the database because there is not enough information to determine
                                              whether the database insert command will fail due to null values.
                                              The database column contains a DefaultValue property but it is misleading because it does not reflect the actual default value in the database.
                                              The only accurate way to determine the default value of a database column is to query the INFORMATION_SCHEMA which only works with MS SQL Server.
                                              Since we are trying to make this database agnostic, we need to avoid querying the INFORMATION_SCHEMA.
                                            */
                                            compareResults.Warnings.Add(String.Format("{0}.{1}  :  Column does not exist in test schema, Db column does not allow null values, Db Column is not set to Auto Increment (Identity), and there is no way to determine if Db column has a default value therefore SqlExceptions may occur when test data is inserted into database.", tblName, colName));
                                    }
                                    else
                                        compareResults.Information.Add(String.Format("{0}.{1}  :  Column does not exist in test schema, but should be okay because Db column allows null values.", tblName, colName));
                            }
                        }
                        catch (SqlException)
                        {
                            compareResults.Errors.Add(String.Format("{0}  :  Table does not exist in database.", tblName));
                        }
                    }
                }

                return compareResults;

            }
        }

    }
}