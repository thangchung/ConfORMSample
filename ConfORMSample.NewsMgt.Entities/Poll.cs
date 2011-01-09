using System;
using ConfORMSample.Core;
using ConfORMSample.NewsMgt.Entities.Contract;

namespace ConfORMSample.NewsMgt.Entities
{
    public class Poll : EntityBase<Guid>, IPoll
    {
        public virtual int Value { get; set; }

        public virtual DateTime VoteDate { get; set; }

        public virtual string WhoVote { get; set; }
    }
}