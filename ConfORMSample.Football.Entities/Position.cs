using System;
using System.Collections.Generic;
using ConfORMSample.Core;
using ConfORMSample.Football.Entities.Contract;

namespace ConfORMSample.Football.Entities
{
    public class Position : EntityBase<Guid>, IPosition
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual ICollection<IPerson> Persons { get; set; }

        public virtual ICollection<IGame> Games { get; set; }

        public Position()
            : this(new HashSet<IPerson>(), new HashSet<IGame>())
        {
        }

        public Position(ICollection<IPerson> persons, ICollection<IGame> games)
        {
            Persons = persons;
            Games = games;
        }
    }
}