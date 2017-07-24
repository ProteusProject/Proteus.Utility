#region License

/*
 * Copyright © 2009-2017 the original author or authors.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests
{
    [TestFixture]
    public class WhenKeyIsFoundInAppConfigAndEnvironmentVariable
    {
        [SetUp]
        public void TestSetUp()
        {
            Environment.SetEnvironmentVariable(ConnectionStringTests.TestKey, ConnectionStringTests.EnvironmentVariableSetting, EnvironmentVariableTarget.Process);
            ExtensibleSourceConfigurationManager.ConnectionStringReaders.Add(EnvironmentVariableReader.GetConnectionString);
        }

        [TearDown]
        public void TestTearDown()
        {
            Environment.SetEnvironmentVariable(ConnectionStringTests.TestKey, string.Empty, EnvironmentVariableTarget.Process);
        }

        [Test]
        public void ValueIsReadFromEnvironment()
        {
            Assume.That(System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringTests.TestKey], Is.Not.Null, $"The value in app.config file for {ConnectionStringTests.TestKey} must not be {AppSettingTests.EnvironmentVariableValue} when this test runs.");

            var value = ExtensibleSourceConfigurationManager.ConnectionStrings(ConnectionStringTests.TestKey);
            Assert.That(value.ConnectionString, Is.EqualTo(ConnectionStringTests.EnvironmentVariableConnectionStringValue));
        }
    }
}
