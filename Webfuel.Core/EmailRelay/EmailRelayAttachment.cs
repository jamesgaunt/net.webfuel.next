using System;
using System.IO;

namespace Webfuel
{
    public class EmailRelayAttachment: IDisposable
    {
        public EmailRelayAttachment(string fileName, Stream stream)
        {
            FileName = fileName;
            Stream = stream;
        }

        public string FileName { get; private set; }

        public Stream Stream { get; private set; }

        public void Dispose()
        {
            Stream.Dispose();
        }
    }
}
