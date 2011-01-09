using System;
using ConfORMSample.Core;
using ConfORMSample.Football.Entities.Contract;

namespace ConfORMSample.Football.Entities
{
    public class Club : EntityBase<Guid>, IClub
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Colours { get; set; }
    }
}