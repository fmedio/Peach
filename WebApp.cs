using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class WebApp<TServices>
    {
        private readonly int _httpPort;
        private readonly TServices _services;
        private readonly Dispatcher<TServices> _dispatcher;
        private readonly StaticFileHandler _staticFileHandler;

        public IVerb<TServices> NotFoundVerb { get; set; }
        public Func<IResource> NotFoundPage { get; set; }
        public Func<string, Exception, IResource> ErrorPage { get; set; }

        public WebApp(int httpPort, DirectoryInfo webRoot, TServices services, IEnumerable<IVerb<TServices>> verbs)
        {
            _httpPort = httpPort;
            _services = services;
            NotFoundVerb = new FourOhFour<TServices>();

            ErrorPage = (message, exception) => new ErrorPage(exception, message);
            NotFoundPage = () => new ForOhFourPage();

            _staticFileHandler = new StaticFileHandler(webRoot, NotFoundPage);

            _dispatcher = new Dispatcher<TServices>(verbs, NotFoundVerb);
        }

        // Under Win7 you need to grant the current user the right to open an HTTP pipeline on all interfaces
        // like so:
        // netsh http add urlacl url=http://+:80/MyUri user=DOMAIN\user

        public void Start()
        {
             var listener = new HttpListener();
            listener.Prefixes.Add("http://+:" + _httpPort + "/");
            listener.Start();
            Console.WriteLine("Listening on port {0}", _httpPort);
            while (true)
            {
                var localContext = listener.GetContext();
                new Thread(() => Service(localContext.Request, localContext.Response)).Start();
            }
        }

        private void Service(HttpListenerRequest request, HttpListenerResponse response)
        {
            IResource resource = null;
            try
            {
                if (request.Url.LocalPath.StartsWith("/static/"))
                {
                    resource = _staticFileHandler.Handle(request.Url.LocalPath);
                } else
                {
                    var localName = Regex.Replace(request.Url.LocalPath, "^/", "");
                    var args = ParseArgs(request.QueryString);
                    var verb = _dispatcher.Dispatch(localName);
                    resource = verb.Action(_services, args);                                                    
                }

            } catch (Exception e)
            {
                resource = ErrorPage("Error executing request", e);
            }

            try
            {
                response.ContentType = resource.ContentType;
                response.StatusCode = resource.HttpStatus;
                resource.ExtraHeaders.ForEach(kvp => response.AddHeader(kvp.Key, kvp.Value));
                resource.Render(response.OutputStream);
                response.OutputStream.Close();
            } catch (Exception e)
            {
                Log.Error(this, "Error rendering resource", e);
            }
        }

        private Bag<string, string> ParseArgs(NameValueCollection nvc)
        {
            var bag = new Bag<string, string>();

            foreach (var key in nvc.AllKeys)
            {
                var values = nvc.GetValues(key);
                foreach (var value in values)
                {
                    if (!String.IsNullOrEmpty(key))
                    {
                        bag[key] = value;
                    }
                }
            }

            return bag;
        }
    }
}