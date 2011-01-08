using System;
using ConfORMSample.Persistence.Entities.Contract;

namespace ConfORMSample.Persistence.Entities
{
    public class News : EntityBase<Guid>, INews
    {
        public virtual string Title { get; set; }

        public virtual string ShortDescription { get; set; }

        public virtual string Content { get; set; }

        public virtual ICategory Category { get; set; }

        public virtual IPoll Poll { get; set; }

        public virtual void AssignPoll(IPoll poll)
        {
            Poll = poll;
        }

        public virtual void AssignCategory(ICategory cat)
        {
            cat.News.Add(this);
            Category = cat;
        }
    }
}