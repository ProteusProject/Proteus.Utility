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
using NUnit.Framework.Internal;

namespace Proteus.Utility.Configuration.Test.AppSettingsTests
{
    [TestFixture]
    public class WhenMultipleSettingsReadersAreAssigned
    {
        [SetUp]
        public void TestSetUp()
        {
            Environment.SetEnvironmentVariable(AppSettingTests.TestKey, AppSettingTests.EnvironmentVariableValue, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(AppSettingTests.EnvironmentVariableOnlyTestKey, AppSettingTests.EnvironmentVariableValue, EnvironmentVariableTarget.Process);
            ExtensibleSourceConfigurationManager.AppSettingReaders.Add(LocalSettingsJsonReader.GetAppSetting);
            ExtensibleSourceConfigurationManager.AppSettingReaders.Add(EnvironmentVariableReader.GetAppSetting);
        }

        [TearDown]
        public void TestTearDown()
        {
            Environment.SetEnvironmentVariable(AppSettingTests.TestKey, string.Empty, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(AppSettingTests.EnvironmentVariableOnlyTestKey, string.Empty, EnvironmentVariableTarget.Process);
        }

        [Test]
        public void CanRespectOrderOfValueResolution()
        {
            var testValue = ExtensibleSourceConfigurationManager.AppSettings(AppSettingTests.TestKey);
            var environmentOnlyValue = ExtensibleSourceConfigurationManager.AppSettings(AppSettingTests.EnvironmentVariableOnlyTestKey);
            var localSettingsJsonOnlyValue = ExtensibleSourceConfigurationManager.AppSettings(AppSettingTests.LocalSettingsJsonOnlyTestKey);

            Assert.That(testValue, Is.EqualTo(AppSettingTests.EnvironmentVariableValue));
            Assert.That(environmentOnlyValue, Is.EqualTo(AppSettingTests.EnvironmentVariableValue));
            Assert.That(localSettingsJsonOnlyValue, Is.EqualTo(AppSettingTests.LocalSettingsJsonValue));
        }
    }
}