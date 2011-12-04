using System.IO;

namespace Peach
{
    public abstract class HtmlSnippet : Tags, IResource
    {
        public abstract IRenderable Contents { get; }

        

        public string ContentType
        {
            get { return "text/html"; }
        }

        public int HttpStatus
        {
            get { return 200; }
        }

        public void Render(Stream stream)
        {
            var streamWriter = new StreamWriter(stream);
            streamWriter.AutoFlush = true;
            Contents.Render(streamWriter);
        }

        public Bag<string, string> ExtraHeaders
        {
            get { return new Bag<string, string>(); }
        }

        
    }
}