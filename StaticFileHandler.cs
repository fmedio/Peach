using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class StaticFileHandler
    {
        private readonly DirectoryInfo _webRoot;
        private readonly Func<IResource> _resourceNotFound;

        public StaticFileHandler(DirectoryInfo webRoot, Func<IResource> resourceNotFound)
        {
            _webRoot = webRoot;
            _resourceNotFound = resourceNotFound;
        }

        public IResource Handle(string target)
        {
            target = Regex.Replace(target, "^/static/", "");
            target = Regex.Replace(target, "/", "\\");

            var localPath = Path.Combine(_webRoot.FullName, target);

            if (! File.Exists(localPath))
            {
                return _resourceNotFound();
            }

            var fileInfo = new FileInfo(localPath);
            var contentType = DetermineContentType(fileInfo);
            return new StaticResource(contentType, fileInfo);
        }

        private string DetermineContentType(FileInfo localPath)
        {
            var extension = localPath.Extension.ToLower();
            if (ContentTypes.ContainsKey(extension))
            {
                return ContentTypes[extension];
            } else
            {
                return "application/octet-stream";
            }
        }

        private static readonly Dictionary<string, string> ContentTypes = new Dictionary<string, string>
                                                                              {
                                                                                  {".js", "text/javascript"},
                                                                                  {".txt", "text/plain"},
                                                                                  {".css", "text/css"},
                                                                                  {".html", "text/html"},
                                                                                  {".xml", "text/xml"},
                                                                                  {".gif", "image/gif"},
                                                                                  {".png", "image/png"},
                                                                                  {".jpg", "image/jpeg"},
                                                                                  {".jpeg", "image/jpeg"},
                                                                                  {".ico", "image/bmp"},
                                                                              }; 
    }

    class StaticResource : IResource
    {
        private readonly FileInfo _path;

        public StaticResource(string contentType, FileInfo path)
        {
            _path = path;
            ContentType = contentType;
        }

        public string ContentType { get; private set; }

        public int HttpStatus { get { return 200; } }

        public void Render(Stream stream)
        {
            var fileStream = new FileStream(_path.FullName, FileMode.Open, FileAccess.Read);
            fileStream.PipeTo(stream);
            fileStream.Close();
        }

        public Bag<string, string> ExtraHeaders { get { return new Bag<string, string>(); } }
    }
}