using System;
using System.Collections.Generic;

namespace ConfORMSample.Persistence.Entities.Contract
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