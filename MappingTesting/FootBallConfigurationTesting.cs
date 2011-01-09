using System;
using System.Collections.Generic;
using ConfORMSample.ConfORM;
using ConfORMSample.ConfORM.NHibernate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Football = ConfORMSample.Football.ConfORM;

namespace ConfORMTesting
{
    [TestClass]
    public class FootBallConfigurationTesting
    {
        private ConfORMConfigBuilder _confOrmConfigBuilder;
        private string _connectionString;
        private IEnumerable<string> _assemblies;

        [TestInitialize]
        public void InitTest()
        {
            _connectionString = "Server=localhost;database=ConfORMSample_FootballMsgt;Integrated Security=SSPI;";

            _assemblies = new List<string>
                              {
                                  "ConfORMSample.Football.Entities.dll"
                              };

            _confOrmConfigBuilder = new Football.SqlServerConfORMConfigBuilder(_assemblies, "ConfORMSample_FootballMsgt.dbo");

            NHibernateSession.Init(_confOrmConfigBuilder, new SimpleSessionStorage(), _connectionString, "Football");
        }

        [TestMethod]
        public void Can_Init_Football_Config()
        {
            Assert.IsNotNull(_confOrmConfigBuilder);
        }

        [TestMethod]
        public void Can_Get_Configuration_From_NHibernate_Session()
        {
            var session = NHibernateSession.CurrentFor("Football");
            Assert.IsNotNull(session);
        }

        [TestCleanup]
        public void CleanUp()
        {
            GC.SuppressFinalize(_confOrmConfigBuilder);
        }
    }
}