using System;
using System.Collections.Generic;

namespace ConfORMSample.Persistence.Entities.Contract
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