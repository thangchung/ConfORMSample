using System;
using System.Collections.Generic;
using ConfORMSample.Common.Entities;
using ConfORMSample.Core;
using ConfORMSample.Football.Entities.Contract;

namespace ConfORMSample.Football.Entities
{
    public class Person : EntityBase<Guid>, IPerson
    {
        public virtual string Name { get; set; }

        public virtual string Gender { get; set; }

        public virtual Address Address { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string HomePhoneNumber { get; set; }

        public virtual string CellPhoneNumber { get; set; }

        public virtual ICollection<IPosition> Positions { get; set; }

        public virtual ICollection<IGame> Games { get; set; }

        public Person()
            : this(new HashSet<IPosition>(), new HashSet<IGame>())
        {
        }

        public Person(ICollection<IPosition> positions, ICollection<IGame> games)
        {
            Positions = positions;
            Games = games;
        }
    }
}