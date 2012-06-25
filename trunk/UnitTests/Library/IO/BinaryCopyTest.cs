namespace VS.Library.UT.IO
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    internal class BinaryCopyTest
    {
        [Test]
        [Repeat(1000)]
        public void CopyUsingBinarySerializer() 
        {
            object obj1 = "abcd";
            string deserializedObj = null;

            var serializer = new BinaryFormatter();
            var buffer = new BlockingCollection<byte>(new ConcurrentQueue<byte>(), 32768);
            using (var readerStream = new ReaderStream(buffer))
            {
                using (var writerStream = new WriterStream(buffer))
                {
// ReSharper disable AccessToDisposedClosure
                    var deserializeTask = Task<string>.Factory.StartNew(() => (string)serializer.Deserialize(readerStream));
// ReSharper restore AccessToDisposedClosure
                    try
                    {
                        serializer.Serialize(writerStream, obj1);
                        buffer.CompleteAdding();
                        deserializedObj = deserializeTask.Result;
                    }
                    catch (Exception exception)
                    {
                        if (!buffer.IsAddingCompleted)
                        {
                            buffer.CompleteAdding();
                        }
                        deserializeTask.Wait();
                    }
                    finally
                    {
                        deserializeTask.Dispose();
                    }
                }
            }
            Assert.AreEqual(obj1, deserializedObj);
        }
    }
}
