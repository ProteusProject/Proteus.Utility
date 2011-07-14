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
using System.Data;
using System.Data.SqlClient;

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
        public IList<string> Errors { get { return _errors; } }

        /// <summary>
        /// Read-only collection of information-level messages returned from the SchemaComparisonReport
        /// </summary>
        public IList<string> Information { get { return _information; } }

        /// <summary>
        /// Read-only collection of warning-level messages returned from the SchemaComparisonReport
        /// </summary>
        public IList<string> Warnings { get { return _warnings; } }

    }
}
