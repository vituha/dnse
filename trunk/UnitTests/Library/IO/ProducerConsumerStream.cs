using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Concurrent;

namespace VS.Library.UT.IO
{
    public class ProducerConsumerStream : Stream
    {
        protected BlockingCollection<byte> queue;

        public ProducerConsumerStream(BlockingCollection<byte> queue)
        {
            this.queue = queue;
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

         public override void Flush()
        {
        }

        protected long length = long.MaxValue;

        public override long Length
        {
            get { return length; }
        }

        public override long Position
        {
            get
            {
                return length;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
