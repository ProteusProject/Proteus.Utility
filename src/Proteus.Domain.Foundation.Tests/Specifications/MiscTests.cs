using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Linq.Expressions;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications.MyShit
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
        private Person _child1;
        private Person _child2;
        private Person _child3;
        private List<Person> _children;
        private Person _notParent;
        private Person _parent;
        private List<Person> _people;

        [FixtureSetUp]
        public void _TestFixtureSetUp()
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

            _people = new List<Person>() { _parent, _notParent, _child1, _child2, _child3 };
        }

        [Test]
        public void CanFindByLastnameSpec()
        {
            var spec = new PersonByLastnameSpecification("Bohlen");
            var results = spec.SatisfyingElementsFrom(_people.AsQueryable());

            Assert.Contains(results.ToList(), _parent);

        }

        [Test]
        public void CanApplySpecToSingleElement()
        {
            var spec = new PersonWithLastNameBohlenSpecification();
            var result = spec.IsSatisfiedBy(_parent);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanMatchSingleByCompoundOrSpec()
        {
            var spec = new PersonByFirstnameOrLastnameSpecification("Johnny", "Bohlen");
            var result = spec.IsSatisfiedBy(_parent);

            Assert.IsTrue(result);

        }

        [Test]
        public void CanFindByCompountOrSpec()
        {
            var spec = new PersonByFirstnameOrLastnameSpecification("Johnny", "Bohlen");
            var results = spec.SatisfyingElementsFrom(_people.AsQueryable());

            Assert.Contains(results.ToList(), _parent);
            Assert.Contains(results.ToList(), _child3);

        }
    }

    public class PersonWithLastNameBohlenSpecification : QuerySpecification<Person>
    {
        public override Expression<Func<Person, bool>> MatchingCriteria
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

        public override bool IsSatisfiedBy(Person candidate)
        {
            return _fnameSpec.IsSatisfiedBy(candidate) || _lnameSpec.IsSatisfiedBy(candidate);
        }

        public override IQueryable<Person> SatisfyingElementsFrom(IQueryable<Person> candidates)
        {

            return _fnameSpec.Or(_lnameSpec).SatisfyingElementsFrom(candidates);
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

        public override Expression<Func<Person, bool>> MatchingCriteria
        {
            get { return p => p.Firstname == _firstname; }
        }

        //public IQueryable<Person> SatisfyingElementsFrom(IQueryable<Person> candidates)
        //{
        //    return candidates.Where(c => c.Firstname == _firstname);
        //}
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

        public override Expression<Func<Person, bool>> MatchingCriteria
        {
            get { return p => p.Lastname == _lastname; }
        }

        //public IQueryable<Person> SatisfyingElementsFrom(IQueryable<Person> candidates)
        //{
        //    return candidates.Where(c => c.Lastname == _lastname);
        //}
    }
}
