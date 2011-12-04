using System;
using System.IO;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class ErrorPage : IResource
    {
        private readonly Exception _exception;
        private readonly string _message;

        public ErrorPage(Exception exception, string message)
        {
            _exception = exception;
            _message = message;
        }

        

        public string ContentType
        {
            get { return "text/plain"; }
        }

        public int HttpStatus
        {
            get { return 503; }
        }

        public void Render(Stream stream)
        {
            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine("Oh noes! Fail!");
            streamWriter.WriteLine(_message);
            streamWriter.WriteLine(_exception.GetType().FullName);
            streamWriter.WriteLine(_exception.StackTrace);
            streamWriter.Flush();
        }

        public Bag<string, string> ExtraHeaders
        {
            get { return new Bag<string, string>(); }
        }

        
    }
}