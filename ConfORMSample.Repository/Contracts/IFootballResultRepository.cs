using System;
using ConfORMSample.Core.Persistence;
using ConfORMSample.Football.Entities;

namespace ConfORMSample.Repository.Contracts
{
    public interface IFootballResultRepository : IRepositoryWithTypedId<Game, Guid>
    {
    }
}