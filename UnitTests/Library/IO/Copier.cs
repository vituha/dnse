using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace VS.Library.UT.IO
{
    [TestFixture]
    class BinaryCopyTest
    {
        [Test]
        [Repeat(1000)]
        public void CopyUsingBinarySerializer() 
        {
            object obj1 = "abcd";
            string deserializedObj;

            var serializer = new BinaryFormatter();
            var buffer = new BlockingCollection<byte>(new ConcurrentQueue<byte>());
            using (var readerStream = new ReaderStream(buffer))
            using (var writerStream = new WriterStream(buffer))
            {

                Task serializeTask = new Task(() => serializer.Serialize(writerStream, obj1));
                serializeTask.Start();
                deserializedObj = (string)serializer.Deserialize(readerStream);
            }
            Assert.AreEqual(obj1, deserializedObj);
        }
    }
}
