using System.IO;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class ForOhFourPage : IResource
    {
        public string ContentType
        {
            get { return "text/plain"; }
        }

        public int HttpStatus
        {
            get { return 404; }
        }

        public void Render(Stream stream)
        {
            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine("Four, oh, four!  Resource not found.");
            streamWriter.Flush();
        }

        public Bag<string, string> ExtraHeaders
        {
            get { return new Bag<string, string>(); }
        }
    }
}