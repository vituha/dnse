using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Concurrent;

namespace VS.Library.UT.IO
{
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
            length += count;
        }

        public override void Close()
        {
            queue.CompleteAdding();
            base.Close();
        }
    }
}
