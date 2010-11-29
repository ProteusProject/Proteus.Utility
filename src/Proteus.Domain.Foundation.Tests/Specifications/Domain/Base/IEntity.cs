using System;

namespace Proteus.Domain.Foundation.Tests.Specifications.Domain.Base
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}