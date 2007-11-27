using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using VS.Library.Diagnostics;
using System.Reflection;
using System.IO;
using System.Diagnostics;

using FixtNS = VS.Library.Cache;
using NUnit.Framework.SyntaxHelpers;

namespace VS.Library.UT.Cache
{
    [TestFixture]
    public class GetterCache : AssertionHelper
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
        }

        [TearDown]
        public void DeInit()
        {
        }

        [Test(Description = "TValue Get<TValue>(D0<TValue> getter)")]
        public void Get()
        {
            getterCallCounter = 0;
            propertyCallCounter = 0;

            Assert.That(SettingsPath, Is.EqualTo(@"C:\MyProduct\MyApp\MyApp.config"));
            Assert.That(ProductPath, Is.EqualTo(@"C:\MyProduct"));
            Assert.That(AppPath, Is.EqualTo(@"C:\MyProduct\MyApp"));

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
        string ProductPath
        {
            get
            {
                propertyCallCounter++;
                return FixtNS.GetterCache.Get<string>(GetProductPath);
            }
        }

        string GetAppPath() { getterCallCounter++; return Path.Combine(ProductPath, AppFolder); }
        string AppPath
        {
            get
            {
                propertyCallCounter++;
                return FixtNS.GetterCache.Get<string>(GetAppPath);
            }
        }

        string GetSettingsPath() { getterCallCounter++; return Path.Combine(AppPath, AppSettingsFile); }
        string SettingsPath
        {
            get
            {
                propertyCallCounter++;
                return FixtNS.GetterCache.Get<string>(GetSettingsPath);
            }
        }

    }
}
