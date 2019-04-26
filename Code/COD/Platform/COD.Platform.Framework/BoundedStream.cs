using System;
using System.IO;

namespace COD.Platform.Framework
{
    public class BoundedStream : Stream
    {
        private Stream myInnerStream;
        private int myStart;
        private long myLength;


        public BoundedStream(Stream innerStream, int start, int length)
        {
            if (innerStream == null) throw new ArgumentNullException("innerStream must not be null");
            if (!(length > 0)) throw new ArgumentException("Length must be greater than zero");
            if (!(start >= 0)) throw new ArgumentException("Start must be greater than zero");
            if (!(start < innerStream.Length)) throw new ArgumentException("Start must be before the end of the innerStream");
            if (!((start + length) <= innerStream.Length)) throw new ArgumentException("End (start + length) must be before the end of the stream");

            MemoryStream ms;
            myInnerStream = innerStream;
            innerStream.Position = start;
            myStart = start;
            myLength = length;
        }

        public override bool CanRead
        {
            get { return myInnerStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return myInnerStream.CanRead; }
        }

        public override bool CanWrite
        {
            get { return myInnerStream.CanRead; }
        }

        public override void Flush()
        {
            myInnerStream.Flush();
        }

        public override long Length
        {
            get { return myLength; }
        }

        public override long Position
        {
            get
            {
                return myInnerStream.Position - myStart;
            }
            set
            {
                if (value < 0) throw new InvalidOperationException("Attempt to set the position before the beginning of the stream");
                if (!(value < Length)) throw new InvalidOperationException("Attempt to set the position past the end of the stream");
                myInnerStream.Position = myStart + value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (Position + count > Length)
                count = (int)(Length - Position);
            return myInnerStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long newPos = myStart;

            var end = myStart + myLength;

            switch (origin)
            {
                case SeekOrigin.Begin:
                    if (offset < 0)
                        throw new InvalidOperationException("Cannot seek to a -ve number from the beginning of the stream");
                    newPos = myStart + offset;
                    break;
                case SeekOrigin.Current:
                    newPos = myInnerStream.Position + offset;
                    break;
                case SeekOrigin.End:
                    if (offset > 0)
                        throw new InvalidOperationException("Cannot seek to a +ve number from the end of the stream");
                    newPos = end + offset;
                    break;
            }

            if (newPos > end)
                throw new InvalidOperationException("The offset would put you past the end of the stream");

            if (newPos < myStart)
                throw new InvalidOperationException("The offset would put you past the beginning of the stream");

            myInnerStream.Position = newPos;
            return Position;
        }

        public override void SetLength(long value)
        {
            if (value + myStart > myInnerStream.Length)
                myInnerStream.SetLength(value + myStart);

            myLength = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (Position + count > myLength)
                throw new InvalidOperationException("Attempt to write past the end of the stream");

            myInnerStream.Write(buffer, offset, count);
        }
    }
}
