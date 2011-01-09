using System;
using ConfORMSample.Common.Entities;
using ConfORMSample.Core;
using ConfORMSample.Football.Entities.Contract;

namespace ConfORMSample.Football.Entities
{
    public class Location : EntityBase<Guid>, ILocation
    {
        public virtual string Name { get; set; }

        public virtual Address Address { get; set; }

        public virtual string Description { get; set; }
    }
}