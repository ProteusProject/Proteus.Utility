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
using System.Configuration;
using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test.AppSettingsTests
{
    [TestFixture]
    public class WhenUsingGenericTypedMethodToRetrieveSetting
    {
        [SetUp]
        public void TestSetUp()
        {
            ExtensibleSourceConfigurationManager.AppSettingReaders.Add(EnvironmentVariableReader.GetAppSetting);
        }


        [TearDown]
        public void TestTearDown()
        {
            SetEnvironmentVariable(string.Empty);
        }


        [Test]
        public void CanRetrieveBooleanValue()
        {
            SetEnvironmentVariable("true");
            Assert.That(ExtensibleSourceConfigurationManager.AppSettings<bool>("test-key"), Is.True);
        }

        [Test]
        public void CanRetrieveNumericValue()
        {
            SetEnvironmentVariable("10");
            Assert.That(ExtensibleSourceConfigurationManager.AppSettings<int>("test-key"), Is.EqualTo(10));
        }

        [Test]
        public void CanRetrieveUriValue()
        {
            var uriString = "http://some/host/some/resource";
            SetEnvironmentVariable(uriString);
            var value = ExtensibleSourceConfigurationManager.AppSettings<Uri>("test-key");

            Assert.That(value, Is.TypeOf<Uri>());
            Assert.That(value.AbsoluteUri, Is.EqualTo(uriString));
        }

        [Test]
        public void ThrowsIfKeyNotFound()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () => ExtensibleSourceConfigurationManager.AppSettings<object>("test-key"));
        }

        [Test]
        public void ThrowsIfInvalidTypeConversion()
        {
            SetEnvironmentVariable("10");

            try
            {
                ExtensibleSourceConfigurationManager.AppSettings<bool>("test-key");
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.That(ex, Is.TypeOf<ConfigurationErrorsException>());
                Assert.That(ex.InnerException, Is.TypeOf<FormatException>());
            }
        }

        private void SetEnvironmentVariable(string value)
        {
            Environment.SetEnvironmentVariable("test-key", value, EnvironmentVariableTarget.Process);
        }
    }
}