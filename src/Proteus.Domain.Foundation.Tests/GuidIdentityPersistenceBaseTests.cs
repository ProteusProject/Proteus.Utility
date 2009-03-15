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
using Proteus.Domain.Foundation;
using Proteus.Utility.UnitTest;
using System.Reflection;
using System.Diagnostics;

namespace Domain.Foundation.Tests
{
    [TestFixture]
    public class GuidIdentityPersistenceBaseTests : Proteus.Utility.UnitTest.UnitTestBase
    {
        [Test]
        public void PersistedInstanceIsNotTransient()
        {
            ClassDerivedFromGuidIdentityPersistenceBase p = new ClassDerivedFromGuidIdentityPersistenceBase();

            //use reflection to set the internal ID of the object to 'simulate' persistence
            SetInstanceFieldValue(p, "_persistenceId", Guid.NewGuid());

            Assert.IsFalse(p.IsTransient);

        }

    }


    internal class ClassDerivedFromGuidIdentityPersistenceBase : GuidIdentityPersistenceBase<ClassDerivedFromGuidIdentityPersistenceBase> {}
    
}
