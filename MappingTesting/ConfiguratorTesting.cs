using System;
using ConfORMSample.Core.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfORMTesting
{
    [TestClass]
    public class ConfiguratorTesting
    {
        private IConfigurator _configurator;

        [TestInitialize]
        public void Init()
        {
            _configurator = new Configurator();
        }

        [TestMethod]
        public void Can_Get_Init_Configurator()
        {
            Assert.IsNotNull(_configurator);
        }

        [TestMethod]
        public void Can_Get_App_Settings()
        {
            var appsettings = _configurator.GetAppSettingString("IsCreateNewDatabase");

            Assert.IsTrue(!string.IsNullOrEmpty(appsettings));
        }

        [TestCleanup]
        public void CleanUp()
        {
            GC.SuppressFinalize(_configurator);
        }
    }
}