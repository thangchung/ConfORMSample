using System.Collections.Generic;
using ConfORMSample.Common.Entities;

namespace ConfORMSample.Football.Entities.Contract
{
    public interface IPerson
    {
        string Name { get; set; }

        string Gender { get; set; }

        Address Address { get; set; }

        string EmailAddress { get; set; }

        string HomePhoneNumber { get; set; }

        string CellPhoneNumber { get; set; }

        ICollection<IPosition> Positions { get; set; }

        ICollection<IGame> Games { get; set; }
    }
}