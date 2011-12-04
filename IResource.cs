using System.IO;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public interface IResource
    {
        string ContentType { get; }
        int HttpStatus { get; }
        Bag<string, string> ExtraHeaders { get; }
        void Render(Stream stream);
    }
}