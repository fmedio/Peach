using System.IO;

namespace Peach
{
    public class Link : Tags, IRenderable
    {
        private readonly IRenderable _renderable;

        private Link(IRenderable renderable)
        {
            _renderable = renderable;
        }

        

        public void Render(StreamWriter streamWriter)
        {
            _renderable.Render(streamWriter);
        }

        

        public static Link To<TServices, TNoun>(TypedVerb<TServices, TNoun> verb, TNoun noun, string value)
        {
            string urlTarget = verb.NounParser.ToUrlTarget(noun);
            return new Link(A(verb.Name + urlTarget, value));
        }

        public static Link To(string id, string value)
        {
            return new Link(A("#", value).Id(id));
        }
    }
}