namespace VS.Library.UT.IO
{
    using System.Collections.Concurrent;

    public class WriterStream : ProducerConsumerStream
    {
        public WriterStream(BlockingCollection<byte> queue)
            : base(queue)
        {
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
            {
                queue.Add(buffer[i]);
            }
        }
    }
}
