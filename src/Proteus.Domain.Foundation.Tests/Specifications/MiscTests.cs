/*
 *
 * Proteus
 * Copyright (C) 2010
 * Stephen A. Bohlen
 * http://www.unhandled-exceptions.com
 *
 * Portions Copyright (C) 2008
 * Steve Burman
 * Linq.Specifications http://code.google.com/p/linq-specifications/
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
using System.Linq.Expressions;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications
{
    public class Address
    {
        public int StreetNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Address Address { get; set; }
        public List<Person> Children { get; set; }

        /// <summary>
        /// Initializes a new instance of the Person class.
        /// </summary>
        public Person()
        {
            Children = new List<Person>();
        }

    }

    [TestFixture]
    public class Tests
    {
        private Person _alsoNotParent;

        private Person _child1;

        private Person _child2;

        private Person _child3;

        private List<Person> _children;

        private Person _notParent;

        private Person _parent;

        private List<Person> _people;

        [TestFixtureSetUp]
        public void _TestTestFixtureSetUp()
        {
            _child1 = new Person() { Firstname = "Suzy", Lastname = "Bohlen" };
            _child2 = new Person() { Firstname = "Timmy", Lastname = "Bohlen" };
            _child3 = new Person() { Firstname = "Johnny", Lastname = "Doe" };

            _children = new List<Person>() { _child1, _child2 };

            _parent = new Person()
                                    {
                                        Firstname = "Steve",
                                        Lastname = "Bohlen",
                                        Address = new Address()
                                        {
                                            City = "New York",
                                            State = "NY",
                                            Street = "Ohio Place",
                                            StreetNumber = 4
                                        },
                                        Children = _children
                                    };

            _notParent = new Person() { Firstname = "Susan", Lastname = "Doe" };
            _alsoNotParent = new Person() { Firstname = "Susan", Lastname = "UniqueLastname" };

            _people = new List<Person>() { _parent, _notParent, _alsoNotParent, _child1, _child2, _child3 };
        }

        [Test]
        public void CanApplySpecToSingleElement()
        {
            var spec = new PersonWithLastNameBohlenSpecification();
            var result = spec.IsSatisfiedBy(_parent);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanFindByCompoundAndSpec()
        {
            var spec = new PersonByFirstnameAndLastnameSpecification("Susan", "UniqueLastname");
            var results = spec.SatisfyingElementsFrom(_people.AsQueryable());

            Assert.That(results.ToList(), Has.Member(_alsoNotParent), "Didn't get the right Susan");
            Assert.That(results.ToList(), Has.No.Member(_notParent), "Got an extra Susan in the results");
        }

        [Test]
        public void CanFindByCompoundOrSpec()
        {
            var spec = new PersonByFirstnameOrLastnameSpecification("Johnny", "Bohlen");
            var results = spec.SatisfyingElementsFrom(_people.AsQueryable());

            Assert.That(results.ToList(), Has.Member(_parent));
            Assert.That(results.ToList(), Has.Member(_child3));
        }

        [Test]
        public void CanFindByLastnameSpec()
        {
            var spec = new PersonByLastnameSpecification("Bohlen");
            var results = spec.SatisfyingElementsFrom(_people.AsQueryable());

            Assert.That(results.ToList(), Has.Member(_parent));

        }

        [Test]
        public void CanMatchSingleByCompoundAndSpec()
        {
            var spec = new PersonByFirstnameOrLastnameSpecification("Susan", "UniqueLastname");
            var result = spec.IsSatisfiedBy(_alsoNotParent);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanMatchSingleByCompoundOrSpec()
        {
            var spec = new PersonByFirstnameOrLastnameSpecification("Johnny", "Bohlen");
            var result = spec.IsSatisfiedBy(_parent);

            Assert.IsTrue(result);

        }

    }

    public class PersonWithLastNameBohlenSpecification : QuerySpecification<Person>
    {
        public override Expression<Func<Person, bool>> Predicate
        {
            get
            {
                return p => p.Lastname == "Bohlen";
            }
        }
    }

    public class PersonByFirstnameOrLastnameSpecification : QuerySpecification<Person>
    {
        private string _firstname;

        private PersonByFirstnameSpecification _fnameSpec;

        private string _lastname;

        private PersonByLastnameSpecification _lnameSpec;

        /// <summary>
        /// Initializes a new instance of the PersonByFirstnameOrLastnameSpecification class.
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        public PersonByFirstnameOrLastnameSpecification(string firstname, string lastname)
        {
            _firstname = firstname;
            _lastname = lastname;

            _fnameSpec = new PersonByFirstnameSpecification(_firstname);
            _lnameSpec = new PersonByLastnameSpecification(_lastname);
        }

        public override Expression<Func<Person, bool>> Predicate
        {
            get { return _fnameSpec.Or(_lnameSpec).Predicate; }
        }
    }

    public class PersonByFirstnameAndLastnameSpecification : QuerySpecification<Person>
    {
        private string _firstname;

        private PersonByFirstnameSpecification _fnameSpec;

        private string _lastname;

        private PersonByLastnameSpecification _lnameSpec;

        /// <summary>
        /// Initializes a new instance of the PersonByFirstnameOrLastnameSpecification class.
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        public PersonByFirstnameAndLastnameSpecification(string firstname, string lastname)
        {
            _firstname = firstname;
            _lastname = lastname;

            _fnameSpec = new PersonByFirstnameSpecification(_firstname);
            _lnameSpec = new PersonByLastnameSpecification(_lastname);
        }

        public override Expression<Func<Person, bool>> Predicate
        {
            get { return _fnameSpec.And(_lnameSpec).Predicate; }
        }
    }

    public class PersonByFirstnameSpecification : QuerySpecification<Person>
    {

        private string _firstname;

        /// <summary>
        /// Initializes a new instance of the PersonByFirstnameSpecification class.
        /// </summary>
        /// <param name="firstname"></param>
        public PersonByFirstnameSpecification(string firstname)
        {
            _firstname = firstname;
        }

        public override Expression<Func<Person, bool>> Predicate
        {
            get { return p => p.Firstname == _firstname; }
        }
    }

    public class PersonByLastnameSpecification : QuerySpecification<Person>
    {

        private string _lastname;

        /// <summary>
        /// Initializes a new instance of the PersonByFirstnameSpecification class.
        /// </summary>
        /// <param name="lastname"></param>
        public PersonByLastnameSpecification(string lastname)
        {
            _lastname = lastname;
        }

        public override Expression<Func<Person, bool>> Predicate
        {
            get { return p => p.Lastname == _lastname; }
        }
    }
}
