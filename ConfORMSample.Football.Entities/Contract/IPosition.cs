using System;
using System.Collections.Generic;
using ConfORMSample.Common.Entities.Contract;

namespace ConfORMSample.Football.Entities.Contract
{
    public interface IPosition : IEntity<Guid>
    {
        string Name { get; set; }

        string Description { get; set; }

        ICollection<IPerson> Persons { get; set; }

        ICollection<IGame> Games { get; set; }
    }
}