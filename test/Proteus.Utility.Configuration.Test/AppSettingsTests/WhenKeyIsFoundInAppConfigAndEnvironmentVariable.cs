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

using System;
using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test.AppSettingsTests
{
    [TestFixture]
    public class WhenKeyIsFoundInAppConfigAndEnvironmentVariable
    {
        [SetUp]
        public void TestSetUp()
        {
            Environment.SetEnvironmentVariable(AppSettingTests.TestKey, AppSettingTests.EnvironmentVariableValue, EnvironmentVariableTarget.Process);
            ExtensibleSourceConfigurationManager.AppSettingReaders.Add(EnvironmentVariableReader.GetAppSetting);
        }

        [TearDown]
        public void TestTearDown()
        {
            Environment.SetEnvironmentVariable(AppSettingTests.TestKey, string.Empty, EnvironmentVariableTarget.Process);

        }

        [Test]
        public void ValueIsReadFromEnvironment()
        {
            Assume.That(System.Configuration.ConfigurationManager.AppSettings[AppSettingTests.TestKey], Is.Not.EqualTo(AppSettingTests.EnvironmentVariableValue), $"The value in app.config file for {AppSettingTests.TestKey} must not be {AppSettingTests.EnvironmentVariableValue} when this test runs.");

            var value = ExtensibleSourceConfigurationManager.AppSettings(AppSettingTests.TestKey);
            Assert.That(value, Is.EqualTo(AppSettingTests.EnvironmentVariableValue));
        }
    }
}