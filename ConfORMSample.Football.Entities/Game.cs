using System;
using System.Collections.Generic;
using ConfORMSample.Core;
using ConfORMSample.Football.Entities.Contract;
using Iesi.Collections.Generic;

namespace ConfORMSample.Football.Entities
{
    public class Game : EntityBase<Guid>, IGame
    {
        public virtual IClub Club { get; set; }

        public virtual ILocation Location { get; set; }

        public virtual IClub OpponentClub { get; set; }

        public virtual ICollection<IPerson> Players { get; set; }

        public virtual ICollection<IPosition> Positions { get; set; }

        public virtual DateTime PlayedDate { get; set; }

        public virtual string Result { get; set; }

        public virtual string Description { get; set; }

        public Game()
            : this(new HashedSet<IPerson>(), new HashedSet<IPosition>())
        {
        }

        public Game(ICollection<IPerson> players, ICollection<IPosition> positions)
        {
            Players = players;
            Positions = positions;
        }
    }
}