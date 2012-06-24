using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Concurrent;

namespace VS.Library.UT.IO
{
    public class ReaderStream : ProducerConsumerStream
    {
        public ReaderStream(BlockingCollection<byte> queue)
            : base(queue)
        {
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int initialOffset = offset;
            foreach (byte item in queue.GetConsumingEnumerable())
	        {
                buffer[offset++] = item;
                if (--count <= 0)
                {
                    break;
                }
            }
            int readCount = offset - initialOffset;
            length += readCount;
            return readCount;
        }
    }
}
