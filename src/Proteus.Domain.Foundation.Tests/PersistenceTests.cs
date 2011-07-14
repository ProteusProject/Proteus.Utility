/*
 *
 * Proteus
 * Copyright (C) 2008, 2009, 2010, 2011
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
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using NHibernate.Criterion;
using Proteus.Utility.UnitTest;

namespace Proteus.Domain.Foundation.Tests
{
    [TestFixture]
    public class PersistenceTests : DatabaseUnitTestBase
    {
        private DataAccessLayer.NHibernateSessionManager _sessionManager;

        [SetUp]
        public void _Setup()
        {
            base.DatabaseSetUp();
        }

        [TearDown]
        public void _TearDown()
        {
            base.DatabaseTearDown();
        }

        [TestFixtureSetUp]
        public void _TestTestFixtureSetUp()
        {

            NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration().Configure();

            NHibernate.Tool.hbm2ddl.SchemaExport schemaExport = new NHibernate.Tool.hbm2ddl.SchemaExport(cfg);

            schemaExport.Drop(false, true);
            schemaExport.Create(false, true);

            _sessionManager = new DataAccessLayer.NHibernateSessionManager();

            ISessionFactory sessionFactory = _sessionManager.GetSession().SessionFactory;

            base.DatabaseTestFixtureSetUp();
        }

        [TestFixtureTearDown]
        public void _TestFixtureTearDown()
        {
            base.DatabaseTestFixtureTearDown();
        }

        [Test]
        public void CanUpdateFirstnameProperty()
        {

            ISession session = _sessionManager.GetSession();

            DetachedCriteria dc = DetachedCriteria.For<Customer>().Add(NHibernate.Criterion.Restrictions.Eq("Lastname", "Bohlen"));

            IList<Customer> customers = dc.GetExecutableCriteria(session).List<Customer>();

            Assert.IsNotNull(customers);

            Customer c = customers[0];

            string originalFirstname = c.Firstname;

            c.Firstname += "zzz";

            session.SaveOrUpdate(c);
            session.Flush();

            session.Evict(c);

            customers = null;

            customers = dc.GetExecutableCriteria(session).List<Customer>();

            Customer updatedCustomer = customers[0];

            Assert.AreEqual(originalFirstname + "zzz", updatedCustomer.Firstname);

        }

        [Test]
        public void MappingCheckTest()
        {
            ISession session = _sessionManager.GetSession();

            DetachedCriteria dc = DetachedCriteria.For<Customer>();

            IList<Customer> customers = dc.GetExecutableCriteria(session).List<Customer>();

            Assert.IsNotNull(customers);

        }

    }
}
