using System;
using System.IO;

namespace Peach
{
    public class TextResource : IResource
    {
        private readonly string _value;

        public TextResource(string value)
        {
            _value = value;
        }

        public string ContentType
        {
            get { return "text/plain; charset=utf8"; }
        }

        public int HttpStatus
        {
            get { return 200; }
        }

        public Bag<string, string> ExtraHeaders
        {
            get { return new Bag<string, string>(); }
        }

        public void Render(Stream stream)
        {
            var writer = new StreamWriter(stream) {AutoFlush = true};
            writer.WriteLine(_value);
        }
    }
}