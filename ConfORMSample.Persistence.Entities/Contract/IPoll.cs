using System;

namespace ConfORMSample.Persistence.Entities.Contract
{
    public interface IPoll : IEntity<Guid>
    {
        int Value { get; set; }

        DateTime VoteDate { get; set; }

        string WhoVote { get; set; }
    }
}