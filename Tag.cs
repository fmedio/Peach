using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class Tag : IRenderable
    {
        private readonly List<KeyValuePair<string, string>> _attributes;
        private readonly List<IRenderable> _children;
        private readonly string _name;

        public Tag(string name)
        {
            _name = name;
            _children = new List<IRenderable>();
            _attributes = new List<KeyValuePair<string, string>>();
        }

        

        public void Render(StreamWriter streamWriter)
        {
            if (_children.Count == 0)
            {
                streamWriter.Write("<" + _name + FormatAttributes() + " />");
            }
            else
            {
                streamWriter.Write("<" + _name + FormatAttributes() + ">");
                _children.ForEach(r => r.Render(streamWriter));
                streamWriter.Write("</" + _name + ">");
            }
        }

        

        private string FormatAttributes()
        {
            if (_attributes.Count == 0)
            {
                return "";
            }

            var stringBuilder = new StringBuilder();
            _attributes.ForEach(kvp =>
                                    {
                                        stringBuilder.Append(" ");
                                        stringBuilder.Append(kvp.Key);
                                        stringBuilder.Append("=\"");
                                        stringBuilder.Append(kvp.Value);
                                        stringBuilder.Append("\"");
                                    });
            return stringBuilder.ToString();
        }

        public Tag Add(params string[] textBits)
        {
            _children.AddRange(textBits.Select(t => new Text(t)));
            return this;
        }

        public Tag Add(IEnumerable<IRenderable> renderables)
        {
            _children.AddRange(renderables);
            return this;
        }

        public Tag Add(params IRenderable[] renderables)
        {
            return Add((IEnumerable<IRenderable>) renderables);
        }

        public Tag Attr(string key, string value)
        {
            _attributes.Add(new KeyValuePair<string, string>(key, value));
            return this;
        }

        public Tag Id(string value)
        {
            return Attr("id", value);
        }
    }
}