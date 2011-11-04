using System.IO;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public interface IRenderable
    {
        void Render(StreamWriter streamWriter);
    }

    public class Text : IRenderable
    {
        private readonly string _value;

        public Text(string value)
        {
            _value = value;
        }

        public void Render(StreamWriter streamWriter)
        {
            streamWriter.Write(System.Security.SecurityElement.Escape(_value));
        }
    }
}