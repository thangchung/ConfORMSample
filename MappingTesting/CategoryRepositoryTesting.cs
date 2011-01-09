using System;
using System.Collections.Generic;
using ConfORMSample.ConfORM;
using ConfORMSample.ConfORM.NHibernate;
using ConfORMSample.Persistence.Entities;
using ConfORMSample.Persistence.Entities.Contract;
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

        private ICategory _category;

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

            _category = AddCategory();
        }

        [TestMethod]
        public void Can_Add_Category()
        {
            var temp = GetCategory(_category.Id);
            Assert.AreEqual(temp.Id, _category.Id);
        }

        [TestMethod]
        public void Can_Update_Category()
        {
            var category = GetCategory(_category.Id);

            category.Name = "Name updated";
            category.CreatedDate = DateTime.Now;
            category.Description = "Description updated";

            var updatedCategory = _categoryRepository.SaveOrUpdate(category);

            Assert.AreEqual(updatedCategory.Name, category.Name);
            Assert.IsTrue(updatedCategory.CreatedDate.Equals(category.CreatedDate));
            Assert.AreEqual(updatedCategory.Description, category.Description);
        }

        [TestMethod]
        public void Can_Delete_Category()
        {
            var temp = GetCategory(_category.Id);
            _categoryRepository.Delete(temp);

            var nullObject = _categoryRepository.Get(_category.Id);

            Assert.IsNull(nullObject);
        }

        [TestCleanup]
        public void CleanUp()
        {
            GC.SuppressFinalize(_confOrmConfigBuilder);
            GC.SuppressFinalize(_categoryRepository);
        }

        private Category AddCategory()
        {
            var category = new Category
            {
                Name = "category name 1",
                CreatedDate = DateTime.Now,
                Description = "Something"
            };

            var dbContext = _categoryRepository.GetDbContext();
            var temp = _categoryRepository.SaveOrUpdate(category);
            dbContext.CommitChanges();

            return temp;
        }

        private Category GetCategory(Guid id)
        {
            return _categoryRepository.Get(id);
        }
    }
}