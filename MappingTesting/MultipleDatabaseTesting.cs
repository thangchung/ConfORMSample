using System;
using System.Collections.Generic;
using ConfORMSample.ConfORM;
using ConfORMSample.ConfORM.NHibernate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Football = ConfORMSample.Football.ConfORM;

namespace ConfORMTesting
{
    [TestClass]
    public class MultipleDatabaseTesting
    {
        private ConfORMConfigBuilder _newsMgtConfigBuilder;
        private string _newsMgtConnectionString;
        private IEnumerable<string> _newsMgtAssemblies;

        private ConfORMConfigBuilder _footballConfigBuilder;
        private string _footballConnectionString;
        private IEnumerable<string> _footballAssemblies;

        private ISessionStorage _sessionStorage;

        [TestInitialize]
        public void Init()
        {
            _sessionStorage = new SimpleSessionStorage();

            // news management config area
            _newsMgtConnectionString = "Server=localhost;database=ConfORMSample;Integrated Security=SSPI;";

            _newsMgtAssemblies = new List<string>
                              {
                                  "ConfORMSample.NewsMgt.Entities.dll"
                              };

            _newsMgtConfigBuilder = new Football.SqlServerConfORMConfigBuilder(_newsMgtAssemblies, "ConfORMSample.dbo");

            NHibernateSession.Init(_newsMgtConfigBuilder, _sessionStorage, _newsMgtConnectionString, "NewsMgt");

            // football management config area
            _footballConnectionString = "Server=localhost;database=ConfORMSample_FootballMsgt;Integrated Security=SSPI;";

            _footballAssemblies = new List<string>
                              {
                                  "ConfORMSample.Football.Entities.dll"
                              };

            _footballConfigBuilder = new Football.SqlServerConfORMConfigBuilder(_footballAssemblies, "ConfORMSample_FootballMsgt.dbo");

            NHibernateSession.Init(_footballConfigBuilder, _sessionStorage, _footballConnectionString, "FootballMgt");
        }

        [TestMethod]
        public void Can_Init_All_Config()
        {
            Assert.IsNotNull(_newsMgtConfigBuilder);
            Assert.IsNotNull(_footballConfigBuilder);
        }

        [TestMethod]
        public void Can_Get_NewsMgt_Session()
        {
            Assert.IsNotNull(NHibernateSession.CurrentFor("NewsMgt"));
        }

        [TestMethod]
        public void Can_Get_FootballMgt_Session()
        {
            Assert.IsNotNull(NHibernateSession.CurrentFor("FootballMgt"));
        }

        [TestCleanup]
        public void CleanUp()
        {
            GC.SuppressFinalize(_newsMgtConfigBuilder);
            GC.SuppressFinalize(_footballConfigBuilder);
            GC.SuppressFinalize(_sessionStorage);
        }
    }
}