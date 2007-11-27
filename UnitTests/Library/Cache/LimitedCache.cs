using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using VS.Library.Diagnostics;
using System.Reflection;
using System.IO;
using System.Diagnostics;

using FixtNS = VS.Library.Generics.Cache;
using NUnit.Framework.SyntaxHelpers;

namespace VS.Library.UT.Cache
{
    [TestFixture]
    public class LimitedCache : AssertionHelper
    {
        FixtNS.LimitedCache<int, string> cache = new FixtNS.LimitedCache<int, string>(100);

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void DeInit()
        {
        }

        [Test(Description = "Intensive Addition")]
        public void Populate()
        {
            for (int i = 0; i < 1000; i++)
            {
                cache.GetDefault(i, "Value " + i.ToString());
            }

            Assert.That(cache.Keys.Count <= 100);
            DumpCache();
        }

        [Test(Description = "Intensive Get")]
        public void Get()
        {
            for (int i = 0; i < 1000; i++)
            {
    //            cache.GetDefault(i, "Value " + i.ToString());
                if (i % 5 == 0)
                {
                    cache.GetDefault(i, "Value " + i.ToString());
                }
                if (i % 25 == 0)
                {
                    cache.GetDefault(i, "Value " + i.ToString());
                }
                if (i % 10 == 0)
                {
                    cache.GetDefault(i, "Value " + i.ToString());
                }
            }

            Assert.That(cache.Keys.Count <= 100);
            DumpCache();
        }

        private void DumpCache()
        {
            Console.WriteLine("==== Cache Dump =====");
            foreach (int key in this.cache)
            {
                Console.WriteLine(String.Format("{0}, {1}", key, this.cache.GetDefault(key, String.Empty)));
            }
            
        }

    }
}
