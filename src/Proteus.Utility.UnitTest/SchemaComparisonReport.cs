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
