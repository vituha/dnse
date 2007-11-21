using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using VS.Library.Diagnostics;
using System.Reflection;
using System.IO;
using VS.Library.Cache;
using System.Diagnostics;

namespace VS.Library.UT.Cache
{
    [TestFixture]
    public class CodeFixture
    {
        private int getterCallCounter;
        private int propertyCallCounter;
        private string Drive = @"C:\";
        private string ProductFolder = "MyProduct";
        private string AppFolder = "MyApp";
        private string AppSettingsFile = "MyApp.config";

        [SetUp]
        public void Init()
        {
            getterCallCounter = 0;
            propertyCallCounter = 0;
        }

        [TearDown]
        public void DeInit()
        {
        }

        [Test]
        public void PropertyCache()
        {
            Assert.AreEqual(SettingsPath, @"C:\MyProduct\MyApp\MyApp.config");
            Assert.AreEqual(ProductPath, @"C:\MyProduct");
            Assert.AreEqual(AppPath, @"C:\MyProduct\MyApp");
            Console.WriteLine(String.Format("Property call count: {0}", propertyCallCounter));
            Console.WriteLine(String.Format("Getters call count: {0}", getterCallCounter));
        }

        string CalcCachedPropValue()
        {
            string res = "*";
            for (int i = 0; i < 1000; i++)
            {
                res += "*";
            }
            return res;
        }


        string GetProductPath() { getterCallCounter++; return Path.Combine(Drive, ProductFolder); }
        string GetAppPath() { getterCallCounter++; return Path.Combine(ProductPath, AppFolder); }
        string GetSettingsPath() { getterCallCounter++; return Path.Combine(AppPath, AppSettingsFile); }

        string ProductPath
        {
            get
            {
                propertyCallCounter++;
                return GetterCache.Get<string>(GetProductPath);
            }
        }

        string AppPath
        {
            get
            {
                propertyCallCounter++;
                return GetterCache.Get<string>(GetAppPath);
            }
        }

        string SettingsPath
        {
            get
            {
                propertyCallCounter++;
                return GetterCache.Get<string>(GetSettingsPath);
            }
        }

    }
}
