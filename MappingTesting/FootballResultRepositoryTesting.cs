using System;
using System.Collections.Generic;
using ConfORMSample.ConfORM;
using ConfORMSample.ConfORM.NHibernate;
using ConfORMSample.Repository;
using ConfORMSample.Repository.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Football = ConfORMSample.Football.ConfORM;

namespace ConfORMTesting
{
    [TestClass]
    public class FootballResultRepositoryTesting
    {
        private ConfORMConfigBuilder _confOrmConfigBuilder;
        private string _connectionString;
        private IEnumerable<string> _assemblies;
        private IFootballResultRepository _repository;

        [TestInitialize]
        public void InitTest()
        {
            _connectionString = "Server=localhost;database=ConfORMSample_FootballMsgt;Integrated Security=SSPI;";

            _assemblies = new List<string>
                              {
                                  "ConfORMSample.Football.Entities.dll"
                              };

            _confOrmConfigBuilder = new Football.SqlServerConfORMConfigBuilder(_assemblies, "ConfORMSample_FootballMsgt.dbo");

            NHibernateSession.Init(_confOrmConfigBuilder, new SimpleSessionStorage(), _connectionString, FootballResultRepository.FootballFactoryKey);

            _repository = new FootballResultRepository();
        }

        [TestMethod]
        public void Can_Get_All_Match_Result()
        {
            var result = _repository.GetAll();
        }

        [TestCleanup]
        public void CleanUp()
        {
            GC.SuppressFinalize(_confOrmConfigBuilder);
        }
    }
}