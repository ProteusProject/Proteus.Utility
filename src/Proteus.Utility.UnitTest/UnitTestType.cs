#region License

/*
*
*  Copyright (c) 2020 Stephen A. Bohlen
*
*  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
*  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
*  and/or sell copies of the Software, and to permit persons to whom the Software is *furnished to do so, subject to the following conditions:
* 
*  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
*  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
*  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
*  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
*  IN THE SOFTWARE.
*
*/

#endregion





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
