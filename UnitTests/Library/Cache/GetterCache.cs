using NUnit.Framework;
using System;
using System.IO;
using FixtNS = VS.Library.Pattern.Lifetime;

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

        string ProductPath
        {
            get
            {
                propertyCallCounter++;
                return FixtNS.GetterCache.Get<string>(
                    delegate { getterCallCounter++; return Path.Combine(Drive, ProductFolder); }
                );
            }
        }

        string AppPath
        {
            get
            {
                propertyCallCounter++;
                return FixtNS.GetterCache.Get<string>(
                    delegate { getterCallCounter++; return Path.Combine(ProductPath, AppFolder); }
                );
            }
        }

        string SettingsPath
        {
            get
            {
                propertyCallCounter++;
                return FixtNS.GetterCache.Get<string>(
                    delegate { getterCallCounter++; return Path.Combine(AppPath, AppSettingsFile); }
                );
            }
        }

    }
}
