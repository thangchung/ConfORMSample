using System;
using ConfORMSample.Persistence.Entities.Contract;

namespace ConfORMSample.Persistence.Entities
{
    public class Poll : EntityBase<Guid>, IPoll
    {
        public virtual int Value { get; set; }

        public virtual DateTime VoteDate { get; set; }

        public virtual string WhoVote { get; set; }
    }
}