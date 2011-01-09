using System;
using ConfORMSample.Common.Entities.Contract;

namespace ConfORMSample.NewsMgt.Entities.Contract
{
    public interface INews : IEntity<Guid>
    {
        string Title { get; set; }

        string ShortDescription { get; set; }

        string Content { get; set; }

        ICategory Category { get; set; }

        IPoll Poll { get; set; }

        void AssignPoll(IPoll poll);

        void AssignCategory(ICategory cat);
    }
}