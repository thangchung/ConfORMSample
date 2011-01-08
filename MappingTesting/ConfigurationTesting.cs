using System;
using System.Collections.Generic;
using System.Linq;
using ConfORMSample.ConfORM;
using ConfORMSample.ConfORM.NHibernate;
using ConfORMSample.Persistence.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfORMTesting
{
    [TestClass]
    public class ConfigurationTesting
    {
        private ConfORMConfigBuilder _confOrmConfigBuilder;
        private string _connectionString;
        private string _sessionFactoryName;
        private IEnumerable<string> _assemblies;

        [TestInitialize]
        public void Init()
        {
            _connectionString = "Server=localhost;database=ConfORMSample;Integrated Security=SSPI;";
            _sessionFactoryName = "SessionFactory1";

            _assemblies = new List<string>
                              {
                                  "ConfORMSample.Persistence.Entities.dll"
                              };

            _confOrmConfigBuilder = new SqlServerConfORMConfigBuilder(_assemblies);

            NHibernateSession.Init(_confOrmConfigBuilder, new SimpleSessionStorage(), _connectionString);
        }

        [TestMethod]
        public void Can_Init_Config()
        {
            var config = _confOrmConfigBuilder.BuildConfiguration(_connectionString, _sessionFactoryName);

            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void Can_Insert_Category()
        {
            var category = new Category
            {
                Name = "category name 1",
                CreatedDate = DateTime.Now,
                Description = "Something"
            };

            var session = NHibernateSession.Current;

            using (var trans = session.BeginTransaction())
            {
                session.Save(category);
                session.Flush();

                trans.Commit();
            }
        }

        [TestMethod]
        public void Can_Init_NHibernate_Session()
        {
            var session = NHibernateSession.Current;

            using (var trans = session.BeginTransaction())
            {
                var result = session.QueryOver<Category>().List();
                var item1 = result.FirstOrDefault();

                var news = new News { Title = "Title 2", ShortDescription = "Sort description 1", Content = "Content 1" };

                var poll = new Poll { Value = 1, WhoVote = "ThangChung", VoteDate = DateTime.Now };

                var user = new User { UserName = "ThangChung", Password = "unknown" };

                news.AssignCategory(item1);

                news.AssignPoll(poll);

                user.AssignPoll(poll);

                session.Save(news);

                session.Save(poll);

                session.Save(user);

                session.Flush();
            }
        }

        [TestMethod]
        public void Can_Delete_Category()
        {
            var session = NHibernateSession.Current;
            using (var trans = session.BeginTransaction())
            {
                var result = session.QueryOver<Category>().List();
                var item1 = result.FirstOrDefault();

                foreach (var news in item1.News)
                {
                    session.Delete(news);
                }

                session.Delete(item1);
                session.Flush();
            }
        }
    }
}