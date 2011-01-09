using System;
using System.Collections.Generic;
using ConfORMSample.Common.Entities.Contract;

namespace ConfORMSample.Football.Entities.Contract
{
    public interface IGame : IEntity<Guid>
    {
        IClub Club { get; set; }

        ILocation Location { get; set; }

        IClub OpponentClub { get; set; }

        ICollection<IPerson> Players { get; set; }

        ICollection<IPosition> Positions { get; set; }

        DateTime PlayedDate { get; set; }

        string Result { get; set; }

        string Description { get; set; }
    }
}