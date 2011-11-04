using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public abstract class Html5Page : Tags, IResource
    {
        public abstract IRenderable BodyContents { get; }
        public abstract string PageTitle { get; }

        public List<string> JsIncludes { get; private set; }
        public List<string> CssIncludes { get; private set; }

        public virtual Bag<string, string> ExtraHeaders { get { return new Bag<string, string>(); } }
        public virtual int HttpStatus { get { return 200; } }


        public string ContentType { get { return "text/html; charset=utf-8"; } }

        protected Html5Page()
        {
            JsIncludes = new List<string>();
            CssIncludes = new List<string>();
        }

        public void Render(Stream stream)
        {
            var streamWriter = new StreamWriter(stream, Encoding.UTF8);
            streamWriter.AutoFlush = true;
            streamWriter.WriteLine("<!DOCTYPE html>");
            Html(
                Head(
                    Concat(CssIncludes.Select(path => Link(path, "stylesheet", "text/css"))),  
                    Concat(JsIncludes.Select(path => Script("javascript", "text/javascript", path))),
                    Title(PageTitle),
                    Body(BodyContents)
                    )
                ).Render(streamWriter);
        }

    }
}