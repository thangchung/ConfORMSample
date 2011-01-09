using System;
using ConfORMSample.Common.Entities.Contract;

namespace ConfORMSample.NewsMgt.Entities.Contract
{
    public interface IPoll : IEntity<Guid>
    {
        int Value { get; set; }

        DateTime VoteDate { get; set; }

        string WhoVote { get; set; }
    }
}