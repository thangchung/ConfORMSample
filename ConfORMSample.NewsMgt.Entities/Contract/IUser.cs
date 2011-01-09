using System;
using System.Collections.Generic;
using ConfORMSample.Common.Entities;
using ConfORMSample.Common.Entities.Contract;

namespace ConfORMSample.NewsMgt.Entities.Contract
{
    public interface IUser : IEntity<Guid>
    {
        string UserName { get; set; }

        string Password { get; set; }

        Address Address { get; set; }

        ICollection<IPoll> Polls { get; set; }

        void AssignPoll(IPoll poll);
    }
}