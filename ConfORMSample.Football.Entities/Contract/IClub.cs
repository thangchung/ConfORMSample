using System;
using ConfORMSample.Common.Entities.Contract;

namespace ConfORMSample.Football.Entities.Contract
{
    public interface IClub : IEntity<Guid>
    {
        string Name { get; set; }

        string Description { get; set; }

        string Colours { get; set; }
    }
}