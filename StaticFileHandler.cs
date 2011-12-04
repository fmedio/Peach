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

        private readonly Func<IResource> _resourceNotFound;
        private readonly DirectoryInfo _webRoot;

        public StaticFileHandler(DirectoryInfo webRoot, Func<IResource> resourceNotFound)
        {
            _webRoot = webRoot;
            _resourceNotFound = resourceNotFound;
        }

        public IResource Handle(string target)
        {
            target = Regex.Replace(target, "^/static/", "");
            target = Regex.Replace(target, "/", "\\");

            string localPath = Path.Combine(_webRoot.FullName, target);

            if (! File.Exists(localPath))
            {
                return _resourceNotFound();
            }

            var fileInfo = new FileInfo(localPath);
            string contentType = DetermineContentType(fileInfo);
            return new StaticResource(contentType, fileInfo);
        }

        private string DetermineContentType(FileInfo localPath)
        {
            string extension = localPath.Extension.ToLower();
            if (ContentTypes.ContainsKey(extension))
            {
                return ContentTypes[extension];
            }
            else
            {
                return "application/octet-stream";
            }
        }
    }

    internal class StaticResource : IResource
    {
        private readonly FileInfo _path;

        public StaticResource(string contentType, FileInfo path)
        {
            _path = path;
            ContentType = contentType;
        }

        

        public string ContentType { get; private set; }

        public int HttpStatus
        {
            get { return 200; }
        }

        public void Render(Stream stream)
        {
            var fileStream = new FileStream(_path.FullName, FileMode.Open, FileAccess.Read);
            fileStream.PipeTo(stream);
            fileStream.Close();
        }

        public Bag<string, string> ExtraHeaders
        {
            get { return new Bag<string, string>(); }
        }

        
    }
}