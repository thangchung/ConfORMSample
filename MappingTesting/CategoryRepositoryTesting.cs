using System;
using System.Collections.Generic;
using System.Linq;
using ConfORMSample.ConfORM;
using ConfORMSample.ConfORM.NHibernate;
using ConfORMSample.Persistence.Entities;
using ConfORMSample.Repository;
using ConfORMSample.Repository.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfORMTesting
{
    [TestClass]
    public class CategoryRepositoryTesting
    {
        private ICategoryRepository _categoryRepository;
        private ConfORMConfigBuilder _confOrmConfigBuilder;
        private string _connectionString;
        private IEnumerable<string> _assemblies;

        [TestInitialize]
        public void Init()
        {
            _categoryRepository = new CategoryRepository();
            _connectionString = "Server=localhost;database=ConfORMSample;Integrated Security=SSPI;";

            _assemblies = new List<string>
                              {
                                  "ConfORMSample.Persistence.Entities.dll",
                                  "ConfORMSample.ConfORM.dll"
                              };

            _confOrmConfigBuilder = new SqlServerConfORMConfigBuilder(_assemblies);

            NHibernateSession.Init(_confOrmConfigBuilder, new SimpleSessionStorage(), _connectionString);
        }

        [TestMethod]
        public void Can_Add_Category()
        {
            var category = new Category
                              {
                                  Name = "category name 1",
                                  CreatedDate = DateTime.Now,
                                  Description = "Something"
                              };

            var dbContext = _categoryRepository.GetDbContext();
            _categoryRepository.SaveOrUpdate(category);
            dbContext.CommitChanges();
        }

        [TestMethod]
        public void Can_Update_Category()
        {
            var categories = _categoryRepository.GetAll();
            _categoryRepository.SaveOrUpdate(categories.FirstOrDefault());
        }

        [TestMethod]
        public void Can_Delete_Category()
        {
            var categories = _categoryRepository.GetAll();
            _categoryRepository.Delete(categories.FirstOrDefault());
        }

        [TestCleanup]
        public void CleanUp()
        {
            GC.SuppressFinalize(_confOrmConfigBuilder);
            GC.SuppressFinalize(_categoryRepository);
        }
    }
}