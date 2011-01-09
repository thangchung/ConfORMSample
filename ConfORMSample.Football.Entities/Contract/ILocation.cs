using System;
using ConfORMSample.Common.Entities;
using ConfORMSample.Common.Entities.Contract;

namespace ConfORMSample.Football.Entities.Contract
{
    public interface ILocation : IEntity<Guid>
    {
        string Name { get; set; }

        Address Address { get; set; }

        string Description { get; set; }
    }
}