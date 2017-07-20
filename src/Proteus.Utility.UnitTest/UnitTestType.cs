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





namespace Proteus.Utility.UnitTest
{
    /// <summary>
    /// Static String values for [Category(...)] attribute to ensure consistency across all TestFixtures
    /// </summary>
    public static class UnitTestType
    {
        /// <summary>
        /// All unit tests with dependencies on interacting with a database
        /// </summary>
        public const string DatabaseDependent = "DatabaseDependent";

        /// <summary>
        /// All unit tests with dependencies on other external applications (e.g. thru COM automation, etc.)
        /// </summary>
        public const string ExternalApplicationDependent = "ExternalApplicationDependent";

        /// <summary>
        /// All unit tests that interact with any other external dependency
        /// </summary>
        public const string Integration = "Integration";

        /// <summary>
        /// All 'pure' unit tests that do not have any external dependencies (intended to be the most common value used)
        /// </summary>
        public const string Unit = "Unit";

        /// <summary>
        /// All unit tests that exercise the user-interface aspects of the code (e.g., WatiN, etc.)
        /// </summary>
        public const string UserInterface = "UserInterface";

        /// <summary>
        /// All long running unit tests, regardless of dependency graph
        /// </summary>
        public const string LongRunning = "LongRunning";

    }
}
