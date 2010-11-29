using System;

namespace Proteus.Domain.Foundation.Tests.Specifications.Domain.Base
{
    public abstract class EntityBase : IEntity
    {
        private Guid id;

        #region IEntity Members

        public virtual Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        #endregion
    }
}