using System;
using ConfORMSample.ConfORM.Repository;
using ConfORMSample.Football.Entities;
using ConfORMSample.Repository.Contracts;
using NHibernate;

namespace ConfORMSample.Repository
{
    public class FootballResultRepository : RepositoryWithTypedId<Game, Guid>, IFootballResultRepository
    {
        public static readonly string FootballFactoryKey = "FootBallMsgtKey";

        protected override ISession GetSession(string factoryKey = "")
        {
            return base.GetSession(FootballFactoryKey);
        }
    }
}