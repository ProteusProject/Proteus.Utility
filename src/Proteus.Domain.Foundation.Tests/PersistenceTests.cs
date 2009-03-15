/*
 *
 * Proteus
 * Copyright (C) 2008, 2009
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
using System.Linq;
using System.Text;
using MbUnit.Framework;
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

        [FixtureSetUp]
        public void _TestFixtureSetup()
        {

            NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration().Configure();

            NHibernate.Tool.hbm2ddl.SchemaExport schemaExport = new NHibernate.Tool.hbm2ddl.SchemaExport(cfg);

            schemaExport.Drop(false, true);
            schemaExport.Create(false, true);

            _sessionManager = new DataAccessLayer.NHibernateSessionManager();

            ISessionFactory sessionFactory = _sessionManager.GetSession().SessionFactory;

            base.DatabaseFixtureSetUp();
        }

        [FixtureTearDown]
        public void _TestFixtureTearDown()
        {
            base.DatabaseFixtureTearDown();
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
