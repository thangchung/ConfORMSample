using System;
using ConfORMSample.ConfORM.Repository;
using ConfORMSample.NewsMgt.Entities;
using ConfORMSample.Repository.Contracts;

namespace ConfORMSample.Repository
{
    public class CategoryRepository : RepositoryWithTypedId<Category, Guid>, ICategoryRepository
    {
    }
}