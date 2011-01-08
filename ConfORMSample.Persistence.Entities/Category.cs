using System;
using System.Collections.Generic;
using ConfORMSample.Persistence.Entities.Contract;
using Iesi.Collections.Generic;

namespace ConfORMSample.Persistence.Entities
{
    public class Category : EntityBase<Guid>, ICategory
    {
        public virtual string Name { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual string Description { get; set; }

        public virtual ICollection<INews> News { get; set; }

        public Category()
            : this(new HashedSet<INews>())
        {
        }

        public Category(ICollection<INews> news)
        {
            News = news;
        }

        public virtual void AssignNews(INews news)
        {
            news.Category = this;
            News.Add(news);
        }
    }
}