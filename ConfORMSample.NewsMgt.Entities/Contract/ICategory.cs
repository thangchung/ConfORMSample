using System;
using System.Collections.Generic;
using ConfORMSample.Common.Entities.Contract;

namespace ConfORMSample.NewsMgt.Entities.Contract
{
    public interface ICategory : IEntity<Guid>
    {
        string Name { get; set; }

        DateTime CreatedDate { get; set; }

        string Description { get; set; }

        ICollection<INews> News { get; set; }

        void AssignNews(INews news);
    }
}